using System;
using UnityEngine;

public interface IAnimatable
{
    public Animator animator{get;set;}
    public Action<string> animationAction{get;set;}
}