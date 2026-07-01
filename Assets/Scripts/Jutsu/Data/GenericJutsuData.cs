using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Jutsu/GenericJutsu")]
public class GenericJutsuData : BaseJutsuData
{
    public List<JutsuEffect> Effects;   // compose freely in the Inspector
    public int chakraCost;
    public float cooldown;
    public AnimationClip animation;

    public override BaseJutsu CreateJutsu() => new GenericJutsu(this);
}
