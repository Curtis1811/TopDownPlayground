using UnityEngine;

[CreateAssetMenu(menuName = "Jutsu/Effects/Dash")]
public class JE_Dash : JutsuEffect
{
    public float distance;
    public override void Apply(JutsuContext jutsuContext)
    {
        jutsuContext.owner.transform.position += jutsuContext.aimDirection * distance * Time.deltaTime;
    }
}