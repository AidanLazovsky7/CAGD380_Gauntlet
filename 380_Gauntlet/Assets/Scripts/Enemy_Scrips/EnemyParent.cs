using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public bool isAttacking = false;

    public bool isDamaging = false;

    protected GameObject player;

    public Collider[] agros;

    private int _numPlayersInAgro;

    [SerializeField] LayerMask playerMask;

    private const int MAXPLAYERS = 4;
    //Game manager for score management

    //private GameManager _gameMannager;

    protected List<iAttack> possibleAttacks = new List<iAttack>();


    protected virtual void Awake()
    {
        //_gameManager = GamemManger.Game;
        SetStats();
        agros = new Collider[MAXPLAYERS];
        StartCoroutine(ScanForPlayers());
    }

    private void FixedUpdate()
    {
        _numPlayersInAgro = Physics.OverlapSphereNonAlloc(transform.position, agroDist, agros, playerMask, QueryTriggerInteraction.Ignore);
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

    private IEnumerator ScanForPlayers()
    {
        while (true)
        {
            CheckAttack();
            yield return new WaitForSeconds(.1f);
        }
    }

    protected virtual void CheckAttack()
    {
        for (int i = 0; i < agros.Length; i++)
        {

            if (agros[i] != null && Vector3.Distance(agros[i].transform.position, this.transform.position) < atkDist && !isAttacking)
            {
                
                Attack();
            }
        }
        
    }
}
