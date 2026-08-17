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
        _context.playerContext.animatable.animator.Play("LightningBladeAttackInit");
        _context.playerContext.animatable.animator.SetTrigger("JutsuStart");
        _currentAnimationStateInfo = _context.playerContext.animatable.animator.GetCurrentAnimatorStateInfo(0);
    }

    public override void UpdateState()
    {
        // Here we want to update our animation list and check if we are at the end of the animation and then exit the state.
        _jutsu.UpdateJutsu(_context);
    }

    public override void ExitState()
    {
        _context.owner.transform.position = _context.owner.transform.position; // Reset position if needed
        _context.playerContext.animatable.animationAction -= AnimationAction;
    }

    public void AnimationAction(string test)
    {
        if (_animationQueue.Count > 0)
        {
            PlayAnimation();
        }
        Debug.Log($"Action FromAnimation We can exit the state now.{test}");
        canTransition = true;
        _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
    }

    private void PlayAnimation()
    {
        if (_animationQueue.Count > 0)
            _context.playerContext.animatable.animator.Play(_animationQueue.Dequeue().name);
    }
}

