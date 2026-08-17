using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseJutsuData : ScriptableObject
{
    public string JutsuName = "New Jutsu";
    public int JutsuDamage;

    public List<AnimationClip> animationList;
    public abstract BaseJutsu CreateJutsu();
}