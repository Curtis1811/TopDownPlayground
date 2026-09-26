using UnityEngine;


public class JumpState : BaseState
{
    IMoveable _moveable;
    Vector3 MovingDirection;
    public int jumpCount = 0;
    float movedirc;

    Animator _animator;

    public JumpState(FSM fsm) : base(fsm)
    {
    }

    public override void PrepareState(CharacterContext context)
    {
        _animator = context.animatable.animator;
        _moveable = context.moveable;
    }

    public override void EnterState()
    {
        Jump();
        movedirc = _moveable.direction.x;
    }

    public override void FixedUpdateState()
    {
        if (_moveable.currentState == IMoveable.State.IsGrounded)
        {
            jumpCount = 0;
            canTransition = true;
        }
        else
        {
            canTransition = false;
        }

        IsSetState();
        AirMovement();
    }

    public override void ExitState()
    {
        jumpCount = 0;
    }

    public void Jump()
    {
        var upforce = 5;

        if (jumpCount == 1)
        {
            upforce = 5;
        }

        if (jumpCount > 1)
        {
            return;
        }

        _moveable.GameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        _moveable.GameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upforce), ForceMode2D.Impulse);
        jumpCount++;
    }

    public void AirMovement()
    {
        var speed = _moveable.speed * Time.deltaTime;

        if (_moveable.direction.x > 0)
        {
            movedirc += 0.01f;

            if (movedirc > _moveable.direction.x)
            {
                movedirc = _moveable.direction.x;
            }
        }
        else if (_moveable.direction.x < 0)
        {
            movedirc -= 0.01f;

            if (movedirc < _moveable.direction.x)
            {
                movedirc = _moveable.direction.x;
            }
        }

        _moveable.GameObject.transform.position += new Vector3(movedirc * speed, 0, 0);
    }

    void IsSetState()
    {
        switch (_moveable.currentState)
        {
            case IMoveable.State.IsFalling:
                _animator.SetBool("isFalling", true);
                _animator.SetBool("isGrounded", false);
                _animator.SetBool("isJumping", false);
                break;
            case IMoveable.State.InAir:
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isGrounded", false);
                break;

            case IMoveable.State.IsGrounded:
                _animator.SetBool("isFalling", false);
                _animator.SetBool("isJumping", false);
                _animator.SetBool("isGrounded", true);
                ChooseExitState();
                break;
        }
    }

    void ChooseExitState()
    {
        Debug.Log(_moveable.direction);
        
        if (_moveable.direction.x != 0)

        {
            _fsm.RequestStateChange(_fsm.StateFactory.MoveState);
        }
        else
        {
            //_fsm.RequestStateChange(_fsm.StateFactory.IdleState);
        }
    }
}