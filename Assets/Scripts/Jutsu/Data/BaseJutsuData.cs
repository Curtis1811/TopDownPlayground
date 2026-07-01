using UnityEngine;

public abstract class BaseJutsuData : ScriptableObject
{
    public string JutsuName = "New Jutsu";
    public int JutsuDamage;
    public AnimationClip animationClip;
    public abstract BaseJutsu CreateJutsu();
}