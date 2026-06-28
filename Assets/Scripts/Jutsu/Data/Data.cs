using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class BaseJutsuData : ScriptableObject
{
    public string JutsuName = "New Jutsu";
    public int JutsuDamage;
    public AnimationClip animationClip;
    public abstract BaseJutsu CreateJutsu();
}
    
[CreateAssetMenu(menuName = "Jutsu/GenericJutsu")]
public class GenericJutsuData : BaseJutsuData
{
    public List<JutsuEffect> Effects;   // compose freely in the Inspector
    public int chakraCost;
    public float cooldown;
    public AnimationClip animation;

    public override BaseJutsu CreateJutsu() => new GenericJutsu(this);
}

[CreateAssetMenu(fileName = "Genjutsu", menuName = "Jutsu/Genjutsu")]
public class Genjutsu : BaseJutsuData
{
    // this will either be the a projectile or the object using this move
    public override BaseJutsu CreateJutsu()
    {
        throw new NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "TaiJutsu", menuName = "Jutsu/Taijutsu")]
public class Taijutsu : BaseJutsuData
{
    // this will either be the a projectile or the object using this move
    public override BaseJutsu CreateJutsu()
    {
        throw new NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Senjutsu", menuName = "Jutsu/Senjutsu")]
public class Senjutsu : BaseJutsuData
{
    // this will either be the a projectile or the object using this move
    public override BaseJutsu CreateJutsu()
    {
        throw new NotImplementedException();
    }
}


/// <summary>
///  Jutsu Effects
/// </summary>

public abstract class JutsuEffect : ScriptableObject
{
    public abstract void Apply(JutsuContext  jutsuContext);
}


[CreateAssetMenu(menuName = "Jutsu/Effects/Dash")]
public class JE_Dash : JutsuEffect
{
    public float distance;
    public override void Apply(JutsuContext jutsuContext)
    {
        jutsuContext.owner.transform.position += jutsuContext.aimDirection * distance * Time.deltaTime;
    }
}