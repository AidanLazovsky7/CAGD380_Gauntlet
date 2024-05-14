using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, iCollectable, iDamageable
{
    public bool breakable = false;

    private int _healing = 100;

    public void pickup(Player user)
    {
        user.addHealth(_healing);
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
        Destroy(this.gameObject);
    }
}
