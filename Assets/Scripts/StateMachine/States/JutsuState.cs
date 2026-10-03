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
    public HitboxHandler _hitboxHandler;
    private AnimationClip _currentAnimation;

    public bool hasPlayedFirstAnim = false;

    public float _timer;

    private int _currentFrame;
    
    /// <summary>
    /// We need some kind of frame data System here to handle the jutsu execution and update and exiting.
    /// </summary>
    /// <param name="fsm"></param>
    public JutsuState(FSM fsm) : base(fsm)
    {
    }

    // TODO: refactor so its not just a player context thing encase we need AI To use aswell.
    public void PrepareJutsu(BaseJutsuData jutsuData, JutsuContext context)
    {
        _jutsuData = jutsuData;
        _context = context;
        _animationQueue.Clear();
        _jutsuData.animationList.ForEach(animation => _animationQueue.Enqueue(animation));
        _hitboxHandler = new HitboxHandler();
    }

    public override void EnterState()
    {
        if (_jutsuData == null || _context == null)
        {
            Debug.LogError("JutsuData or Context is null. Cannot enter JutsuState.");
            return;
        }
        //_hitboxHandler.CreateHitbox(_jutsuData.hitHitbox[0].position, _jutsuData.hitHitbox[0].size, _jutsuData.damage);

        _jutsu = _jutsuData.CreateJutsu();
        _jutsu.StartJutsu(_context);
        canTransition = false;
    }

    public override void FixedUpdateState()
    {
        // Here we want to update our animation list and check if we are at the end of the animation and then exit the state.
        _timer += Time.deltaTime;
        
        if (_currentAnimation != null)
        {
            _currentFrame = Mathf.FloorToInt(_timer * _currentAnimation.frameRate);

            Debug.Log($"FrameRate: {_currentAnimation.length * _currentAnimation.frameRate}" + $"Animation Queue Count: {_animationQueue.Count}");
            Debug.Log($"Current Frame: {_currentFrame} / {_currentAnimation.length * _currentAnimation.frameRate}");
        }

        //Debug.Log($"Animation Queue Count: {_animationQueue.Count}");
        if (!hasPlayedFirstAnim)
        {
            PlayAnimation();
            hasPlayedFirstAnim = true;
        }
        else if (_currentAnimation != null && _currentAnimation.length <= _timer)
        {
            PlayAnimation();
            AnimationAction();
        }

        if (updateEffect)
        {
            _jutsu.UpdateJutsu(_context);
        }
    }

    public override void ExitState()
    {
        _context.owner.transform.position = _context.owner.transform.position; // Reset position if needed
        updateEffect = false;
        canTransition = false;
        _context.CharacterContext.animatable.animator.StopPlayback();
    }

    public void AnimationAction()
    {
        if (_animationQueue.Count == 1)
        {
            updateEffect = true;
        }
        else
        {
            updateEffect = false;
        }
    }

    private void PlayAnimation()
    {
        if (_animationQueue.Count > 0)
        {
            _currentAnimation = _animationQueue.Dequeue();
            _context.CharacterContext.animatable.animator.Play(_currentAnimation.name, 0, 0f);
            _timer = 0;
        }
        else
        {
            canTransition = true;
            updateEffect = false;
            _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
        }
    }
}