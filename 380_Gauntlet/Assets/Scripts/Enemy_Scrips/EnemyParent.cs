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

    public bool isMoving = false;

    protected GameObject player;

    public GameObject projectile;

    public Collider[] agros;

    protected int _numPlayersInAgro;

    private MeshRenderer myRenderer;

    private bool inVisableList = false;

    [SerializeField] LayerMask playerMask;

    private const int MAXPLAYERS = 4;
    //Game manager for score management

    //private GameManager _gameMannager;

    protected List<iAttack> possibleAttacks = new List<iAttack>();

    protected List<iMovement> possibleMovements = new List<iMovement>();


    protected virtual void Awake()
    {
        
        myRenderer = this.GetComponent<MeshRenderer>();
        SetStats();
        agros = new Collider[MAXPLAYERS];
        StartCoroutine(ScanForPlayers());
        GameManager.Instance.visibleEnemies.Add(this.gameObject);
    }

    private void FixedUpdate()
    {
        _numPlayersInAgro = Physics.OverlapSphereNonAlloc(transform.position, agroDist, agros, playerMask, QueryTriggerInteraction.Ignore);
    }

    protected virtual void SetStats()
    {
        
        health = level;
        damage = 5 + (level * 2 - 1);
    }


    // Impliment state meachines for enemeis here
    public abstract void Move(int moveType, int enemy);

    public abstract void Attack(int i);


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
            CheckMove();     
            inVisableList = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.visibleEnemies.Remove(this.gameObject);
        inVisableList = false;
    }

    protected abstract void CheckAttack();

    protected abstract void CheckMove();

    public void AttackPlayer()
    {
        if (isDamaging)
        {
            for (int i = 0; i < agros.Length; i++)
            {

                if (agros[i] != null)
                {
                    float dist = Vector3.Distance(agros[i].transform.position, transform.position);
                    if (dist < atkDist) agros[i].GetComponent<Player>().takeDamage(damage);

                }
            }

        }
    }

}
