using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : EnemyParent
{
    private int _numHits = 0;
    protected override void Awake()
    {
        base.Awake();
        SetStats();
        SetAttackTypes();
        SetMovementTypes();
    }

    protected override void SetStats()
    {
        damage = 1;
        health = level;
        score[0] = 0;
        score[1] = 0;
        score[2] = 100;
        score[3] = 0;
        atkSpd = .1f;
        atkDuration = .05f;
        moveSpd = 2;
        agroDist = 20f;
        atkDist = 8f;
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<MeleeAttack>());
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
        possibleAttacks[i].ExecuteAttackPattern(atkSpd, atkDuration);
    }

    protected override void CheckAttack()
    {
        for (int i = 0; i < agros.Length; i++)
        {

            if (agros[i] != null && !isAttacking)
            {
                if (Vector3.Distance(agros[i].transform.position, this.transform.position) < atkDist)
                    Attack(0);
            }
        }

    }

    protected override void CheckMove()
    {
        for (int i = 0; i < agros.Length; i++)
        {

            if (agros[i] != null && !isMoving)
            {
                if (Vector3.Distance(agros[i].transform.position, this.transform.position) < agroDist)
                {
                    isMoving = true;
                    Move(0, i);
                }

            }
        }

    }


    public override void TakeDamage(int damage, AttackType mytype)
    {
        _numHits++;
        score[2] = ScoreRotation();
        if (mytype == AttackType.Magic)
        {
            health -= damage;
            
            if (health <= 0) OnDeath();
        }
      
    }

    private int ScoreRotation()
    {
        int newScore = 1000;
        switch (_numHits % 7) 
        {
            case 0:
                newScore = 8000;
                break;

            case 1:
                newScore = 1000;
                break;
            case 2:
                newScore = 2000;
                break;
            case 3:
                newScore = 1000;
                break;
            case 4:
                newScore = 4000;
                break;
            case 5:
                newScore = 2000;
                break;
            case 6:
                newScore = 6000; 
                break;
            default:
                break;
        }
        return newScore;
    }
}
