using UnityEngine;

public class StateFactory
{
    FSM _fsm;

    public BaseState IdleState;
    public BaseState MoveState;
    public BaseState JumpState;
    
    public JutsuState JutsuState;


    public StateFactory(FSM fsm)
    {
        _fsm = fsm;
        IdleState = CreateIdleState();
        MoveState = CreateMoveState();  
        JumpState = CreateJumpState();
        JutsuState = CreateJutsuState();
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
}
