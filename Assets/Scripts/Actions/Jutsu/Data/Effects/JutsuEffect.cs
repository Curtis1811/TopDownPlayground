using UnityEngine;

/// <summary>
///  Jutsu Effects
/// </summary>

public abstract class JutsuEffect : ScriptableObject
{
    public abstract void Apply(JutsuContext  jutsuContext);
}