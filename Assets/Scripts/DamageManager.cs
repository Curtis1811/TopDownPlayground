
using UnityEngine;

public static class DamageManager
{
    // Start is called before the first frame update
    static DamageManager()
    {
        
    }
    
    public static void DealDamage(IDealDamage damangeDealer, IDamageable damageable)
    {
        if (damangeDealer == null || damageable == null) return;

        damageable.health -= damangeDealer.damage;

        if (damageable.health <= 0)
        {
            DamageableDeath(damageable);
        }
    }

    private static void DamageableDeath(IDamageable damageable)
    {
        // Handle death logic here (e.g., play animation, drop loot, etc.)
        // play some of death animation and maybe run loot table. and maybe add EXP.
        damageable.OnDeath();
        Debug.Log("Damageable entity has died.");
    }
    
}
