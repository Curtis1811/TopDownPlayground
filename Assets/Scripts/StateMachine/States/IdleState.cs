using System;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(FSM fsm) : base(fsm)
    {
        canTransition = true;
    }

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        Debug.Log("IdleState update");
    }
}
