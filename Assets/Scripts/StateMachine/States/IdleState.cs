using System;
using UnityEngine;

public class IdleState : BaseState
{
    CharacterController _context;
    
    public IdleState(FSM fsm) : base(fsm)
    {
        canTransition = true;
    }

    public override void PrepareState(CharacterContext context)
    {
      
    }

    public override void EnterState()
    {
       
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdateState()
    {
        //Debug.Log("IdleState update");
    }
}
