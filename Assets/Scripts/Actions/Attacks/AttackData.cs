using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Attack/AttackData")]
public class AttackData : ActionData
{
    public enum AttackType
    {
        Light,
        Medium,
        Heavy
    }
    public AnimationClip animation;
}

[System.Serializable]
public class HitboxFrame
{
    public float startFrame;
    public float endFrame;
    public Vector2 position;
    public Vector2 size;
}