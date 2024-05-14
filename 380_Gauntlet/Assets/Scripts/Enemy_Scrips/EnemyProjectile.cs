using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    //sloppy for now so it exists, we can try out lobbing projectiles later
    protected override IEnumerator Move()
    {
        while (myMove)
        {
            
            transform.position += transform.forward * 0.025f * shotSpeed;
            yield return new WaitForSeconds(0.025f);
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerProjectile>() == null)
        {
            if (collision.gameObject.GetComponent<Player>() != null)
            {
                collision.gameObject.GetComponent<Player>().takeDamage(damage);
            }
            else if (collision.gameObject.GetComponent<iDamageable>() != null && collision.gameObject.GetComponent<EnemyParent>() == null)
            {
                collision.gameObject.GetComponent<iDamageable>().TakeDamage(damage, AttackType.Missile);
            }
            Destroy(this.gameObject);
        }
       
    }

    private IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
