using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JutsuState : BaseState
{
    private BaseJutsuData _jutsuData;
    private JutsuContext _context;
    private BaseJutsu _jutsu;
    private Queue<AnimationClip> _animationQueue = new Queue<AnimationClip>();
    public int startFrame = 0;
    public int endFrame = 0;
    public bool updateEffect = false;
    
    AnimatorStateInfo _currentAnimationStateInfo;

    /// <summary>
    /// We need some kind of frame data System here to handle the jutsu execution and update and exiting.
    /// </summary>
    /// <param name="fsm"></param>
    public JutsuState(FSM fsm) : base(fsm)
    {
    }

    public void PrepareJutsu(BaseJutsuData jutsuData, JutsuContext context)
    {
        _jutsuData = jutsuData;
        _context = context;
        _context.playerContext.animatable.animationAction += AnimationAction;
        _animationQueue.Clear();
        _jutsuData.animationList.ForEach(animation => _animationQueue.Enqueue(animation));
    }

    public override void EnterState()
    {
        if (_jutsuData == null || _context == null)
        {
            Debug.LogError("JutsuData or Context is null. Cannot enter JutsuState.");
            return;
        }

        _jutsu = _jutsuData.CreateJutsu();
        _jutsu.StartJutsu(_context);
        canTransition = false;
        _context.playerContext.animatable.animator.SetTrigger("JutsuStart");
    }

    public override void UpdateState()
    {
        // Here we want to update our animation list and check if we are at the end of the animation and then exit the state.
    
        if(updateEffect)
        {
            _jutsu.UpdateJutsu(_context);
        }

        Debug.Log(_animationQueue.Count);
        if (_context.playerContext.animatable.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            PlayAnimation();
        }
        
    }

    public override void ExitState()
    {
        _context.owner.transform.position = _context.owner.transform.position; // Reset position if needed
        _context.playerContext.animatable.animationAction -= AnimationAction;
        canTransition = false;
        _context.playerContext.animatable.animator.StopPlayback();
    }

    public void AnimationAction(string eventString)
    {

        if (eventString == "AttackStart")
        {
            updateEffect = true;
        }
        else
        {
            canTransition = true;
            updateEffect = false;
        }
    }

    private void PlayAnimation()
    {
        if (_animationQueue.Count > 0)
        {
            var animation = _animationQueue.Dequeue();
            _context.playerContext.animatable.animator.Play(animation.name, 0, 0f);
        }
        else
        {
            _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
            canTransition = true;
            updateEffect = false;
        }
    }
}