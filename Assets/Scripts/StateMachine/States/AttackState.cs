using UnityEngine;

namespace StateMachine.States
{
    public class AttackState : BaseState
    {
        
        private PlayerContext _context;
        private AttackData _attackData;
        private bool _hasPlayedNextAnimation = false;
        private Hitbox _hitbox;
        
        public AttackState(FSM fsm) : base(fsm)
        {
            
        }

        public void PrepareAttack(PlayerContext context, AttackData attackData)
        {
            _context = context;
            _attackData = attackData;
            _hitbox = new Hitbox();
            _hitbox.CreateHitbox(new Vector3(0,0), _attackData.hitbox);
        }
        
        public override void EnterState()
        {
            _context.animatable.animator.Play(_attackData.animation.name);
        }

        public override void UpdateState()
        {
            Debug.Log(_context.animatable.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
           // Debug.Log(_context.animatable.animator.GetCurrentAnimatorStateInfo(0).);
            
            if (_context.animatable.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_hasPlayedNextAnimation)
            {
                canTransition = true;
                _hasPlayedNextAnimation = true; 
                _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
                Debug.Log( "StateChange to IdleState");
                
            }else if (_context.animatable.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                _hasPlayedNextAnimation = false;
            }
            
            if (GetCurrentAnimationFrame() >= _attackData.hitboxFrameActivation && GetCurrentAnimationFrame() <= _attackData.hitboxFrameDeactivation)
            {
                Debug.Log("Activate Hitbox");
                _hitbox.ActivateHitbox();
            }
            
             if (GetCurrentAnimationFrame() > _attackData.hitboxFrameDeactivation)
             {
                 Debug.Log("Deactivate Hitbox");
                 _hitbox.DeactivateHitbox();
             }
        
            //_attackData.hitboxFrameActivation;

            //_attackData.hitboxFrameDeactivation;
        }

        public override void ExitState()
        { 
            _context.animatable.animator.StopPlayback();
        }
        
        public float GetCurrentAnimationFrame()
        {
            AnimatorStateInfo stateInfo = _context.animatable.animator.GetCurrentAnimatorStateInfo(0);
            AnimationClip currentClip = _context.animatable.animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            Debug.Log("Current Clip Length: " + currentClip.name);
            // normalizedTime is 0-1, multiply by frame count to get current frame
            int frameCount = (int)(currentClip.length * currentClip.frameRate);
            int currentFrame = (int)(stateInfo.normalizedTime * frameCount) % frameCount;
            //Debug.Log( "Frame Count"  + frameCount);
            //Debug.Log("Current Frame" + currentFrame);
            return currentFrame;
        }
    }
}