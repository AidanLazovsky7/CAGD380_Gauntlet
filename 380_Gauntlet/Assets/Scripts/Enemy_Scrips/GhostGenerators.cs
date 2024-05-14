using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGenerators : GeneratorParent
{
    public override void TakeDamage(int damage, AttackType atkType)
    {
        if (atkType != AttackType.Magic)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) OnDeath();
        }
    }
}
