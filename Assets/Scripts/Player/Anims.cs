using UnityEngine;

public class Anims
{
    private Animator _animator;

    public Anims(Animator animator)
    {
        _animator = animator;
    }

    public void PlayAnimation(string animationName)
    {
        _animator.Play(animationName);
    }

   
}