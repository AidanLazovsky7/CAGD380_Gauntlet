using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int damage;
    protected float shotSpeed;
    protected GameObject model;

    protected bool myMove = true;

    protected void Awake()
    {
        StartCoroutine(Move());
    }

    protected abstract IEnumerator Move();


    protected abstract void OnCollisionEnter(Collision collision);
}
