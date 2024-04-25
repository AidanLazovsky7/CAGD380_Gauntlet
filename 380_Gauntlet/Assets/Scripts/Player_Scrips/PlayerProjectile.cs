using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    protected override IEnumerator Move()
    {
        while (myMove)
        {
            transform.position += transform.forward * 0.025f * 3 * shotSpeed;
            yield return new WaitForSeconds(0.025f);
        }
    }

    //uncomment this when we merge!
    protected override void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<iDamageable>() != null)
        {
            collision.gameObject.GetComponent<iDamageable>().TakeDamage(damage, AttackType.Missile);
        }
        Destroy(this.gameObject);
    }
}
