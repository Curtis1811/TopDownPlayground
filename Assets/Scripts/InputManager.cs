using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    PlayerInputAction inputActions = new();

    public Action<Vector2> MoveAction;
    public Action JumpAction;
    public Action JutsuoneAction;
    
    public Action<int> LightAttackAction;
    public Action<int> MediumAttackAction;
    public Action<int> HeavyAttackAction;

    public InputManager()
    {
        inputActions.Enable();
        inputActions.PlayerAction.Movement.performed += ctx => MovePerformed(ctx);
        inputActions.PlayerAction.Movement.canceled += ctx => MoveCancled(ctx);
        inputActions.PlayerAction.Jump.performed += ctx => JumpPerformed();
        
        inputActions.PlayerAction.LightAttack.performed += ctx => LightAttackPerformed();
        inputActions.PlayerAction.MediumAttack.performed += ctx => MediumAttackPerformed();
        inputActions.PlayerAction.HeavyAttack.performed += ctx => HeavyAttackPerformed();
        
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

    public void LightAttackPerformed()
    {
        LightAttackAction?.Invoke(0);
    }
    
    public void MediumAttackPerformed()
    {
        MediumAttackAction?.Invoke(1);
     }

    public void HeavyAttackPerformed()
    {
        HeavyAttackAction?.Invoke(2);
    }
}
