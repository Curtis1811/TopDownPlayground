using UnityEngine;

public class MoveState : BaseState
{
    IMoveable _moveable;

    public MoveState(FSM fsm) : base(fsm)
    {
        canTransition = true;
    }

    public override void EnterState()
    {
        _moveable = _fsm._moveable;
    }

    public override void UpdateState()
    {
        Move();
        Debug.Log("MoveState Update");
    }

    public override void ExitState()
    {
        
    }

    public void Move()
    {
        var speed = _moveable.speed * Time.deltaTime;
        
        if(_moveable.direction.x  == -1)
        {
            _moveable.GameObject.transform.eulerAngles = new Vector3(0,180,0);
        }
        else if(_moveable.direction.x == 1)
        {
            _moveable.GameObject.transform.eulerAngles = new Vector3(0,0,0);
        }

        _moveable.GameObject.transform.position += new Vector3(_moveable.direction.x * speed, 0, 0); 
    }
}
