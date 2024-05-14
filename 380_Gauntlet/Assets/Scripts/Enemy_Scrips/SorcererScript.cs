using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererScript : EnemyParent
{
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
        atkSpd = 1f;
        atkDuration = .25f;
        moveSpd = 2f;
        agroDist = 9f;
        atkDist = 2.5f;
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<MeleeAttack>());
    }

    private void SetMovementTypes()
    {
        possibleMovements.Add(gameObject.AddComponent<BlinkMovement>());
    }

    public override void Move(int moveType, int enemy)
    {
        possibleMovements[moveType].ExecuteMovementPattern(agros[enemy].gameObject, 0f, agroDist * 1.5f);
    }

    public override void Attack(int i)
    {
        possibleAttacks[i].ExecuteAttackPattern(atkSpd, atkDuration);
    }

    protected override void CheckAttack()
    {
        //for all of my targets
        for (int i = 0; i < agros.Length; i++)
        {
            //if there is one, and i'm not attacking
            if (agros[i] != null && !isAttacking)
            {
                //if i'm within my attack distance, perform an attack.
                if (Vector3.Distance(agros[i].transform.position, this.transform.position) < atkDist)
                    Attack(0);
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
                //check if it's close enough to me
                float dist = Vector3.Distance(agros[i].transform.position, gameObject.transform.position);

                if (dist < agroDist)
                {
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
