using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, iCollectable, iDamageable
{
    public bool breakable = true;

    public void pickup(Player user)
    {
        user.getPotion();
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage, AttackType atkType)
    {
        if (breakable)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        GameManager.Instance.usePotion(1);
        Destroy(this.gameObject);
    }
}
