using StateMachine.States;
using UnityEngine;

public class StateFactory
{
    FSM _fsm;

    public BaseState IdleState;
    public BaseState MoveState;
    public BaseState JumpState;
    public BaseState AttackState;
    
    public JutsuState JutsuState;
    


    public StateFactory(FSM fsm)
    {
        _fsm = fsm;
        IdleState = CreateIdleState();
        MoveState = CreateMoveState();  
        JumpState = CreateJumpState();
        JutsuState = CreateJutsuState();
        AttackState = CreateAttackAction();
    }

    private BaseState CreateIdleState()
    {
        return new IdleState(_fsm);
    }

    private BaseState CreateMoveState()
    {
        return new MoveState(_fsm);
    }

    private BaseState CreateJumpState()
    {
        return new JumpState(_fsm);
    }

    private JutsuState CreateJutsuState()
    {
        return new JutsuState(_fsm);
    }

    public BaseState CreateAttackAction()
    {
        return new AttackState(_fsm);
    }
}
