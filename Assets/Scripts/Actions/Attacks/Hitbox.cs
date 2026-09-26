using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour, IDealDamage
{
    public int damage { get; set; } 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.health -= damage;
            Debug.Log("Hitbox Damage: " + damage);
            Debug.Log("Target Health: " + damageable.health);
        }
    }
    
}
