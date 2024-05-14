using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : EnemyParent
{
    protected override void Awake()
    {
        base.Awake();
        SetStats();
        SetAttackTypes();
        SetMovementTypes();
    }

    protected override void SetStats()
    {
        health = level;
        damage = 10 * level;
        score[0] = 0;
        score[1] = 10;
        score[2] = 10;
        score[3] = 10;
        atkSpd = .5f;
        atkDuration = 1f;
        moveSpd = 4f;
        agroDist = 16f;
        atkDist = 1f;      
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<KamakazeeAttack>());
    }

    private void SetMovementTypes()
    {
        possibleMovements.Add(gameObject.AddComponent<NormalMovement>());

    }

    public override void Move(int moveType, int enemy)
    {
        possibleMovements[0].ExecuteMovementPattern(agros[enemy].gameObject, 0, agroDist);
    }

    public override void Attack(int i)
    {
        possibleAttacks[i].ExecuteAttackPattern(0, 0);
    }

    protected override void CheckAttack()
    {
        for (int i = 0; i < agros.Length; i++)
        {
            if (agros[i] != null && !isAttacking)
            {
                float dist = Vector3.Distance(agros[i].transform.position, this.transform.position);
                if (dist < atkDist)
                {
                    isAttacking = true;
                    Attack(0);
                }
                    

                

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

                if (dist < agroDist)
                {
                    isMoving = true;
                    Move(0, i);
                }

            }
        }
    }

    public void KamakazeeNow()
    {
        for (int i = 0; i < agros.Length; i++)
        {
            if (agros[i])
            {
                float dist = Vector3.Distance(agros[i].transform.position, gameObject.transform.position);

                if (dist < atkDist)
                {
                        agros[i].GetComponent<Player>().takeDamage(damage);
                        Destroy(this.gameObject);
                }
            }
            
        }
    }

    public override void TakeDamage(int damage, AttackType mytype)
    {
        health -= damage;
        if (health <= 0) OnDeath();
    }
}
