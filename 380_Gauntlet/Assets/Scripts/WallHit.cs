using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour
{
    public BreakableWall breakableWall;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
        {
            breakableWall.TakeDamage(1, AttackType.Missile);
        }
    }
}
