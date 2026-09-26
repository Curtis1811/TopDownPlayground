using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseJutsuData : ActionData
{
    public string jutsuName = "New Jutsu";

    public List<AnimationClip> animationList;
    public abstract BaseJutsu CreateJutsu();
}