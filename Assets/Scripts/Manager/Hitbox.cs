using UnityEngine;


public class Hitbox
{
    BoxCollider2D hitboxCollider;
    bool isActive;


    public Hitbox()
    {
        //Pass In Information for out hitbox 
    }

    public void CreateHitbox(Vector2 position, Vector2 size)
    {
        //Create a hitbox
        hitboxCollider = new BoxCollider2D();
        // hitboxCollider.offset = position;
        // hitboxCollider.size = size;
        Debug.DrawLine(position, position + Vector2.right * size.x, Color.red);
        //Debug.DrawLine(position, position + Vector2.right * size.x, Color.red);
        Debug.DrawLine(position, position + Vector2.up * size.y, Color.red);
        //Debug.DrawLine(position, position + Vector2.right * size.x, Color.red);
        
    }

    public void ActivateHitbox()
    {
        // Set Hitbox to active
    }

    public void DeactivateHitbox()
    {
        //Deactivate Hitbox
    }
}