using UnityEngine;
using UnityEngine.Analytics;

public class JumpState : BaseState
{
    IMoveable _moveable;
    Vector3 MovingDirection;
    public int jumpCount = 0;
    float movedirc;

    public JumpState(FSM fsm) : base(fsm)
    {
        
    }

    public override void EnterState()
    {
        _moveable = _fsm._moveable;
        Jump();
        Debug.Log("JumpState Enter");
        movedirc = _moveable.direction.x;
    } 

    public override void UpdateState()
    {
        Debug.Log("JumpState Update");
        if(_moveable.currentState == IMoveable.State.IsGrounded)
        {
            //_moveable.GameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(MovingDirection.x, 0)*0.2f, ForceMode2D.Impulse);
            //_moveable.Direction = new Vector  3(0,0,0);
            canTransition = true;
        }
        else
        {
            canTransition = false;
        }
        
        AirMovement();
    }

    public override void ExitState()
    {
        jumpCount = 0;
        _moveable.direction = new Vector3(0,0,0);
    }

    public void Jump()
    {
        var upforce = 5;

        if(jumpCount == 1)
        {
            upforce = 5;
        }
        if(jumpCount > 1){
            return;
        }

        _moveable.GameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        _moveable.GameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upforce), ForceMode2D.Impulse);
        jumpCount++;
    }

    public void AirMovement()
    {
        var speed = _moveable.speed * Time.deltaTime;

        if(_moveable.direction.x > 0)
        {  
            movedirc += 0.01f;
            
            if(movedirc > _moveable.direction.x)
            {
                movedirc = _moveable.direction.x;
            }
        }
        else if(_moveable.direction.x < 0)
        {
            movedirc -= 0.01f;

            if(movedirc < _moveable.direction.x)
            {
                movedirc = _moveable.direction.x;
            }
        }

        _moveable.GameObject.transform.position += new Vector3(movedirc * speed, 0, 0);
    }
}
