using System;
using UnityEngine;


public class PlayerController
{
    public InputManager inputManager;
    private IMoveable _moveable;
    private FSM _fsm;
    private Animator _animator;
    
    public event Action OnJutsuOneInput;
    
    public PlayerController(IMoveable moveable, IAnimatable animator)
    {
        _moveable = moveable;
        inputManager = new InputManager();
        inputManager.MoveAction += MoveAction;
        inputManager.JumpAction += JumpAction;
        inputManager.JutsuoneAction += () => OnJutsuOneInput?.Invoke();
        
        
        _animator = animator.animator;
        _fsm = new FSM(moveable);
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

    public void JutsuAction(BaseJutsuData data, JutsuContext context)
    {
        _fsm.StateFactory.JutsuState.PrepareJutsu(data,context);
        
        if (_fsm.RequestStateChange(_fsm.StateFactory.JutsuState))
        {
            
        }
    }
    
}