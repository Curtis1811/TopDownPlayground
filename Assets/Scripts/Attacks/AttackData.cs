using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Attack/AttackData")]
public class AttackData : ScriptableObject
{
    public enum AttackType
    {
        Light,
        Medium,
        Heavy
    }
    
    public int Damage;
    public int HitStun;
    public int BlockStun; // Maybe the same as hitstun just on block;

    public Vector2 hitbox;
    public float hitboxFrameActivation;
    public float hitboxFrameDeactivation;
        
    public AnimationClip animation;
    
}
        