using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public float shotSpeed;

    protected bool myMove = true;

    protected void Awake()
    {
        StartCoroutine(Move());
    }

    protected abstract IEnumerator Move();


    protected abstract void OnCollisionEnter(Collision collision);
}
