using UnityEngine;

public class Animations
{
    Animator _animator;
    
    public Animations(Animator animator)
    {
        // This is the constructor for our animayort
        _animator = animator;
        Debug.Log("Animation Constructor");
    }

    public void PlayAnimation(string animationName)
    {
        // This is the method to play the animation
        _animator.Play(animationName);
    }

}