using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    PlayerInputAction inputActions = new();

    public Action<Vector2> MoveAction;
    public Action JumpAction;
    public Action JutsuoneAction;

    public InputManager()
    {
        inputActions.Enable();
        inputActions.PlayerAction.Movement.performed += ctx => MovePerformed(ctx);
        inputActions.PlayerAction.Movement.canceled += ctx => MoveCancled(ctx);
        inputActions.PlayerAction.Jump.performed += ctx => JumpPerformed();
        
        // We are going to move some of this to a input System.
        inputActions.PlayerAction.JutsuOne.performed += ctx => JutsuOnePerformed();
    }
    

    private void JumpPerformed()
    {
        JumpAction?.Invoke();
    }

    public void MovePerformed(InputAction.CallbackContext ctx)
    {    
        MoveAction?.Invoke(ctx.ReadValue<Vector2>());
    }

    public void MoveCancled(InputAction.CallbackContext ctx)
    {   
       MoveAction?.Invoke(ctx.ReadValue<Vector2>());
    }

    public void JutsuOnePerformed()
    {
        JutsuoneAction?.Invoke();
    }
}
