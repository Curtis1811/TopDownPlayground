using UnityEngine;

namespace StateMachine.States
{
    public class AttackState : BaseState
    {
        private CharacterContext _context;
        private AttackData _attackData;
        private HitboxHandler _hitboxHandler;
        private Collider2D hitbox;

        private bool _hasPlayedNextAnimation = false;
        public float timer = 0f;


        public AttackState(FSM fsm) : base(fsm)
        {
        }

        // TODO :This will need to change to an entity context so AI can use
        public void PrepareAttack(CharacterContext context, Collider2D hitbox, AttackData attackData)
        {
            _context = context;
            _attackData = attackData;
            this.hitbox = hitbox;
        }

        public override void EnterState()
        {
            timer = 0;
            _context.animatable.animator.Play(_attackData.animation.name);
            
            _hitboxHandler = new HitboxHandler();
            _hitboxHandler.CreateHitbox(_attackData.hitHitbox[0].position, 
                _attackData.hitHitbox[0].size, _attackData.damage, hitbox);
            _hitboxHandler.ActivateHitbox();
            
            canTransition = false;
            Debug.Log(_attackData.animation.length);
            // just for testing when we enter the state we will create a hitbox and 
            // test the dmg od the attack
        }

        public override void FixedUpdateState()
        {
            timer += Time.deltaTime;

            if (timer > _attackData.animation.length)
            {
                canTransition = true;
                _hasPlayedNextAnimation = true;
                _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
            }
        }


        public override void ExitState()
        {
            _context.animatable.animator.StopPlayback();
            _hitboxHandler.DeactivateHitbox();
            _hitboxHandler = null;
        }
    }
}