using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobberScript : EnemyParent
{
    int rangedAtkDist = 8;
    int currentAgro = 0;

    protected override void Awake()
    {
        base.Awake();
        SetStats();
        SetAttackTypes();
        SetMovementTypes();
    }

    private void SetStats()
    {
        score[0] = 25;
        score[1] = 5;
        score[2] = 10;
        score[3] = 10;
        atkSpd = .4f;
        atkDuration = 1f;
        moveSpd = 1.5f;
        //for this enemy specifically, the agro dist will be used for the range in which the lobber will shoot you
        agroDist = 17f;
        //and this atk dist will be used for their scare radius
        atkDist = 4f;
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<LobAttack>());
    }

    private void SetMovementTypes()
    {
        possibleMovements.Add(gameObject.AddComponent<RunAway>());

    }

    public override void Move(int moveType, int enemy)
    {
        if (moveType == 0) possibleMovements[0].ExecuteMovementPattern(agros[enemy].gameObject, 0f, atkDist * 1.5f);
    }

    //this is extremely wonky but i'm basically passing the attack the position of the aggro we're using so i can lob the projectile
    public override void Attack(int i)
    {
        possibleAttacks[i].ExecuteAttackPattern(atkSpd, atkDuration);
        if (possibleAttacks[i] as LobAttack)
        {
            LobAttack thisAttack = (LobAttack) possibleAttacks[i];
            thisAttack._myTarget = agros[currentAgro].gameObject;
        }
    }

    protected override void CheckAttack()
    {
        for (int i = 0; i < agros.Length; i++)
        {
            currentAgro = i;
            //if i'm aggrod onto something, and i'm not attacking
            if (agros[i] != null && !isAttacking)
            {
                //if they're within my agro range, but not too close
                float dist = Vector3.Distance(agros[i].transform.position, this.transform.position);
                if (dist < agroDist && dist > atkDist)
                {
                    //lobber attack
                    Attack(0);
                }
            }
        }
    }

    protected override void CheckMove()
    {
        for (int i = 0; i < agros.Length; i++)
        {
            //if there is an aggro, and i'm not moving already
            if (agros[i] != null && !isMoving)
            {
                //check if it's too close to me
                float dist = Vector3.Distance(agros[i].transform.position, gameObject.transform.position);

                if (dist < atkDist)
                {
                    //start running!
                    isMoving = true;
                    Move(0, i);
                }
                else isMoving = false;
            }
        }
    }

    public override void TakeDamage(int damage, AttackType mytype)
    {
        health -= damage;
        if (health <= 0) OnDeath();
    }
}
