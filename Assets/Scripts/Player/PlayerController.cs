using System;
using StateMachine.States;
using UnityEngine;


public class PlayerController
{
    public InputManager inputManager;
    private IMoveable _moveable;
    private FSM _fsm;
    private Animator _animator;
    private PlayerContext _playerContext;
    public event Action OnJutsuOneInput;
    public event Action<int> OnLightAttackAction;
    public event Action<int> OnMediumAttackAction;
    public event Action<int> OnHeavyAttackAction;

    public PlayerController(PlayerContext playerContext)
    {
        _moveable = playerContext.moveable;
        _animator = playerContext.animatable.animator;
        _playerContext = playerContext;
        inputManager = new InputManager();
        inputManager.MoveAction += MoveAction;
        inputManager.JumpAction += JumpAction;
        inputManager.JutsuoneAction += () => OnJutsuOneInput?.Invoke();
        
        inputManager.LightAttackAction += (index) => OnLightAttackAction?.Invoke(index);
        inputManager.MediumAttackAction += (index) => OnMediumAttackAction?.Invoke(index);
        inputManager.HeavyAttackAction += (index) => OnHeavyAttackAction?.Invoke(index);

        _fsm = new FSM(_moveable);
    }

    public void Update()
    {
        _fsm.UpdateState();
    }

    void MoveAction(Vector2 direction)
    {
        _moveable.direction = new Vector3(direction.x, direction.y, _moveable.position.z);

        if (direction == Vector2.zero)
        {
            _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
        }

        if (direction != Vector2.zero)
        {
            if (_fsm.RequestStateChange(_fsm.StateFactory.MoveState))
            {
                _animator.SetFloat("MoveSpeed", 2);
                return;
            }
        }

        if (direction == Vector2.zero)
        {
            _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
        }

        _animator.SetFloat("MoveSpeed", 0);
    }

    void JumpAction()
    {
        if (_fsm.RequestStateChange(_fsm.StateFactory.JumpState))
        {
            _moveable.currentState = IMoveable.State.InAir;
            _animator.SetTrigger("Jump");
            _animator.SetBool("Grounded", false);
        }

        Debug.Log("Jump from player");
    }
    
    #region Attacks
    
    public void JutsuAction(BaseJutsuData data, JutsuContext context)
    {
        _fsm.StateFactory.JutsuState.PrepareJutsu(data, context);
        _fsm.RequestStateChange(_fsm.StateFactory.JutsuState);
    }

    public void AttackAction(int attackIndex, AttackData data, PlayerContext playerContext)
    {
        (_fsm.StateFactory.AttackState as AttackState).PrepareAttack(playerContext, data);
        _fsm.RequestStateChange(_fsm.StateFactory.AttackState);
    }
    
    #endregion

    public String GetFSMState()
    {
        return _fsm.GetCurrentState();
    }
}