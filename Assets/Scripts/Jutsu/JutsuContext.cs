using UnityEngine;

public class JutsuContext
{
    public static JutsuContext FromCaster(GameObject owner, Vector3 direction, PlayerContext playerContext)
    {
        return new JutsuContext
        {
            owner = owner,
            aimDirection = direction,
            playerContext = playerContext
        };
    }
    
    public GameObject owner;
    public Vector3 aimDirection;
    public PlayerContext playerContext;
    
}

// public static JutsuContext FromCaster(GameObject owner, Vector3 direction)
// {
//     return new JutsuContext
//     {
//         owner = owner,
//         aimDirection = direction
//     };
// }