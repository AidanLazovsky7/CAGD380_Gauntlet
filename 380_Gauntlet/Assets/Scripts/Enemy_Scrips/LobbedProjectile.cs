using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbedProjectile : EnemyProjectile
{
    private GameObject _myTarget;

    private float grav = -9.8f;

    //we're gonna take one second ig
    private float t = 1f;

    //get the rigidbody
    private Rigidbody m_Rigidbody;
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        base.Awake();
    }

    public void lob(GameObject target)
    {
        _myTarget = target;
        StartCoroutine(Move());
    }

    protected override IEnumerator Move()
    {
        //fire once only
        if (myMove && _myTarget != null)
        {
            //by looking at the target, we've set it up to be like a 2d plane we can do some calcs on
            transform.LookAt(_myTarget.transform);

            //now we know how far away it is from us
            float dist = Vector3.Distance(_myTarget.transform.position, this.transform.position);
            //shoot myself up, and at an angle to try to reach it
            //set my angle
            this.transform.Rotate(-60f, 0f, 0f);
            //set my force
            //THE BALLISTICS EQUATION
            //im not a physicist so it's not 100% accurate, but it's pretty accurate
            float force = 60f * Mathf.Sqrt(dist * -grav * Mathf.Sin(Mathf.PI / 3 * 2));

            //launch that thing
            m_Rigidbody.AddForce(transform.forward * force);
            //transform.position = Vector3.Lerp(this.transform.position, _myTarget, 0.1f);
            yield return new WaitForSeconds(0f);
        }
    }
}
