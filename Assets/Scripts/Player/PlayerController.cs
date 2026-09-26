using System;
using StateMachine.States;
using UnityEngine;


public class PlayerController
{
    public InputManager inputManager;
    private IMoveable _moveable;
    private FSM _fsm;
    private Animator _animator;
    private CharacterContext _characterContext;
    public event Action OnJutsuOneInput;
    public event Action<int> OnLightAttackAction;
    public event Action<int> OnMediumAttackAction;
    public event Action<int> OnHeavyAttackAction;

    bool IsDirectionHeld;
    private Collider2D _hitbox;

    public PlayerController(CharacterContext characterContext, Collider2D hitbox = null)
    {
        _moveable = characterContext.moveable;
        _animator = characterContext.animatable.animator;
        _characterContext = characterContext;
        _hitbox = hitbox;
        inputManager = new InputManager();
        inputManager.MoveAction += MoveAction;
        inputManager.JumpAction += JumpAction;
        inputManager.JutsuoneAction += () => OnJutsuOneInput?.Invoke();

        inputManager.LightAttackAction += (index) => OnLightAttackAction?.Invoke(index);
        inputManager.MediumAttackAction += (index) => OnMediumAttackAction?.Invoke(index);
        inputManager.HeavyAttackAction += (index) => OnHeavyAttackAction?.Invoke(index);

        _fsm = new FSM(_moveable);
    }

    public void FixedUpdate()
    {
        _fsm.FixedUpdateState();

        if (_moveable.direction == Vector2.zero && _fsm.GetCurrentState() != "IdleState")
        {
            _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
        }

        if (_moveable.direction != Vector2.zero)
        {
            if (_fsm.RequestStateChange(_fsm.StateFactory.MoveState))
            {
                _animator.SetFloat("MoveSpeed", 2);
                return;
            }
        }

        _animator.SetFloat("MoveSpeed", 0);
    }


    void MoveAction(Vector2 direction)
    {
        // Everything here needs to get removed or changed as we shouldnt be figuring out what out state machine should do at this stage
        _moveable.direction = new Vector3(direction.x, direction.y, _moveable.position.z);
        _animator.SetFloat("MoveSpeed", 0);
    }

    void JumpAction()
    {
        _fsm.StateFactory.JumpState.PrepareState(_characterContext);

        if (_fsm.RequestStateChange(_fsm.StateFactory.JumpState))
        {
            _moveable.currentState = IMoveable.State.InAir;
        }

        Debug.Log("Jump from player");
    }

    #region Attacks

    public void JutsuAction(BaseJutsuData data, JutsuContext context)
    {
        _fsm.StateFactory.JutsuState.PrepareJutsu(data, context);
        _fsm.RequestStateChange(_fsm.StateFactory.JutsuState);
    }

    public void AttackAction(int attackIndex, AttackData data, CharacterContext characterContext)
    {
        (_fsm.StateFactory.AttackState as AttackState).PrepareAttack(characterContext, _hitbox, data);
        _fsm.RequestStateChange(_fsm.StateFactory.AttackState);
    }

    #endregion

    public String GetFSMState()
    {
        return _fsm.GetCurrentState();
    }
}