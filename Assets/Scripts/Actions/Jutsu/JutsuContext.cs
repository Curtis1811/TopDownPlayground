using UnityEngine;

public class JutsuContext
{
    public static JutsuContext FromCaster(GameObject owner, Vector3 direction, CharacterContext characterContext)
    {
        return new JutsuContext
        {
            owner = owner,
            aimDirection = direction,
            CharacterContext = characterContext
        };
    }
    
    public GameObject owner;
    public Vector3 aimDirection;
    public CharacterContext CharacterContext;
    
}

// public static JutsuContext FromCaster(GameObject owner, Vector3 direction)
// {
//     return new JutsuContext
//     {
//         owner = owner,
//         aimDirection = direction
//     };
// }