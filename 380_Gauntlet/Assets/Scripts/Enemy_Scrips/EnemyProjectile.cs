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

        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().takeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
