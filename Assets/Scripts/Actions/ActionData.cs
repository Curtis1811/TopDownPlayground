using System.Collections.Generic;
using UnityEngine;

public class ActionData : ScriptableObject
{
    public int damage;
    public int hitStun;
    public int blockStun; // Maybe the same as hitstun just on block;

    //public List<AnimationClip> animationList;
    public HitboxFrame[] hitHitbox;
}