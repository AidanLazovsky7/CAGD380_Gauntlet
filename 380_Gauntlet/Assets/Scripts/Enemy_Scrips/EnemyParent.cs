using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyParent : MonoBehaviour, iDamageable, iEnemy
{
    //ints for enemy class
    protected int[] score = new int[4];
    protected int health;
    protected int damage;

    public int level;

    //floats for enemy class

    protected float atkSpd;
    protected float atkDuration;
    protected float moveSpd;
    protected float agroDist;
    protected float atkDist;

    private bool _agro;

    public bool isAttacking;

    protected GameObject player;

    private BoxCollider agroTrigger;
    //Game manager for score management

    //private GameManager _gameMannager;

    protected List<iAttack> possibleAttacks = new List<iAttack>();


    protected virtual void Awake()
    {
        agroTrigger = this.AddComponent<BoxCollider>();
        agroTrigger.size = new Vector3(agroDist, 1, agroDist);
        agroTrigger.isTrigger = true;
        //_gameManager = GamemManger.Game;
        SetStats();



    }

    private void SetStats()
    {
        
        health = level;
        damage = 5 + (level * 2 - 1);
    }


    // Impliment state meachines for enemeis here
    public abstract void Move();

    public abstract void Attack();


    public abstract void TakeDamage(int damage, AttackType atkType);

    //interface inherited function for death and score management
    public void OnDeath()
    {
        //_gamemanager.AddScore(score);
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        
         if (other.GetComponent<Player>())
         {
              _agro = true;
            player = other.gameObject;
         }
         
         

    }

    protected void OnTriggerExit(Collider other)
    {
        
         if (other.GetComponent<Player>())
         {
              _agro = false;
            player = null;
         }
         
         
    }

    protected void OnCollisionStay(Collision collision)
    {
        
         if (collision.gameObject.GetComponent<Player>() && isAttacking)
         {
                isAttacking = false;
              collision.gameObject.GetComponent<Player>().takeDamage(damage);
         }
        else
        {
            Attack();
        }
         
    }
}
