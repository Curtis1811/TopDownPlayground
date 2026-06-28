using UnityEngine;

public class JutsuContext
{
    public static JutsuContext FromCaster(GameObject owner, Vector3 direction)
    {
        return new JutsuContext
        {
            owner = owner,
            aimDirection = direction
        };
    }
    
    public GameObject owner;
    // Not sure if we need a target here has we in fighting games it depends on who we hit.
    public Vector3 aimDirection;
}

// public static JutsuContext FromCaster(GameObject owner, Vector3 direction)
// {
//     return new JutsuContext
//     {
//         owner = owner,
//         aimDirection = direction
//     };
// }