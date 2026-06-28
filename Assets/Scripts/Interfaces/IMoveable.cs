using System;
using UnityEngine;

public interface IMoveable : IEntity
{
    public Vector3 position{ get; set; }
    public Vector3 direction { get; set; }
    public float speed { get; }
    public bool isGrounded { get; set; }
    public State currentState { get; set; }
    
    
    public enum State{
        InAir,
        IsFalling,
        IsGrounded,
        JumpInit,
    }
}
