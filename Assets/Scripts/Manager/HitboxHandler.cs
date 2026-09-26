using UnityEngine;


public class HitboxHandler
{
    BoxCollider2D hitboxCollider;
    GameObject hitboxObject;
    bool isActive;

    public HitboxHandler()
    {
        //Pass In Information for out hitbox 
    }

    public void CreateHitbox(Vector2 position, Vector2 size, int damage, Collider2D hitbox = null)
    {
        if (hitbox == null)
        {
            Debug.Log("Hitbox null");
            return;
        }

        hitboxCollider = hitbox as BoxCollider2D;
        hitboxCollider.offset = position;
        hitboxCollider.size = size;
        hitboxCollider.GetComponentInParent<Hitbox>().damage = damage;
        Debug.Log("Creating a hitbox");
        //Create a hitbox
    }

    public void ActivateHitbox()
    {
        if (!isActive && hitboxCollider != null)
        {
            hitboxCollider.enabled = true;
            isActive = true;
            Debug.Log("Activating hitbox");
        }
    }

    public void DeactivateHitbox()
    {
        //Deactivate Hitbox
        if (isActive && hitboxCollider != null)
        {
            hitboxCollider.enabled = false;
            isActive = false;
            Debug.Log("Deactivating hitbox");
        }
    }
}