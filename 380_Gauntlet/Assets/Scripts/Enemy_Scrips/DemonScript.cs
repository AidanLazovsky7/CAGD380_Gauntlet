using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : EnemyParent
{
    int rangedAtkDist = 8;

    protected override void Awake()
    {
        base.Awake();
        SetStats();
        SetAttackTypes();
        SetMovementTypes();
    }

    protected override void SetStats()
    {
        base.SetStats();
        score[0] = 25;
        score[1] = 5;
        score[2] = 10;
        score[3] = 10;
        atkSpd = .25f;
        atkDuration = 1f;
        moveSpd = 1.5f;
        agroDist = 16f;
        atkDist = 1f;
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<MeleeAttack>());
        possibleAttacks.Add(gameObject.AddComponent<RangedAttack>());
    }

    private void SetMovementTypes()
    {
        possibleMovements.Add(gameObject.AddComponent<NormalMovement>());
        
    }

    public override void Move(int moveType, int enemy)
    {
        if (moveType == 0) possibleMovements[0].ExecuteMovementPattern(agros[enemy].gameObject, atkDist * 4f, agroDist);
        else possibleMovements[0].ExecuteMovementPattern(agros[enemy].gameObject, 0, atkDist * 2.5f);
    }

    public override void Attack(int i)
    {
        possibleAttacks[i].ExecuteAttackPattern(atkSpd, atkDuration);
    }

    protected override void CheckAttack()
    {
        for (int i = 0; i < agros.Length; i++)
        {
            if (agros[i] != null && !isAttacking)
            {
                float dist = Vector3.Distance(agros[i].transform.position, this.transform.position);
                if (dist < atkDist)
                    Attack(0);
                else if (dist < rangedAtkDist && dist > (atkDist * 2) ) Attack(1);
            }
        }
    }

    protected override void CheckMove()
    {

        for (int i = 0; i < agros.Length; i++)
        {
            
            if (agros[i] != null && !isMoving)
            {
                float dist = Vector3.Distance(agros[i].transform.position, gameObject.transform.position);
                
                if (dist < agroDist && dist > 2.5 * atkDist)
                {
                    isMoving = true;
                    Move(0, i);
                }
                else if (dist < atkDist * 2.5f)
                {
                    isMoving = true;
                    Move(1, i);
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
