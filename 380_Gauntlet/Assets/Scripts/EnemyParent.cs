using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyParent : MonoBehaviour, iDamageable
{
    //ints for enemy class
    protected int score;

    //floats for enemy class
    protected float health;
    protected float damage;
    protected float atkSpd;
    protected float moveSpd;
    protected float agroDist;

    private bool _agro;

    //Game manager for score management

    /*
    
    private GameManager _gameMannager;
     
     */


    protected virtual void Awake()
    {
        this.AddComponent<SphereCollider>();
        this.GetComponent<SphereCollider>().radius = agroDist;
        //_gameManager = GamemMnager.Game;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Set all values above here
    }

    protected virtual void Move()
    { 
        // Impliment state meachines for enemeis here
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <=0)
            OnDeath();
    }

    //interface inherited function for death and score management
    public void OnDeath()
    {
        //_gamemanager.AddScore(score);
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        /*
         if (other.GetComponent<Player>())
         {
              _argo = true;
         }
         
         */

    }

    protected void OnTriggerExit(Collider other)
    {
        /*
         if (other.GetComponent<Player>())
         {
              _argo = false;
         }
         
         */
    }

    protected void OnCollisionEnter(Collision collision)
    {
        /*
         if (collision.GetComponent<Player>())
         {
              collision.GetComponent<Player>().TakeDamage(damage);
         }    
         */
    }
}
