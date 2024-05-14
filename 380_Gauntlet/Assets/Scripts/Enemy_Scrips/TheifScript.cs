using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheifScript : EnemyParent
{

    private bool stoleItem = false;
    public int runTime;
    private UpgradeType upgradeType;
    [SerializeField]
    private List<GameObject> _myDrops;

    protected override void Awake()
    {
        base.Awake();
        SetStats();
        SetAttackTypes();
        SetMovementTypes();
    }

    protected override void SetStats()
    {
        score[0] = 25;
        score[1] = 5;
        score[2] = 10;
        score[3] = 10;
        atkSpd = 1f;
        atkDuration = .25f;
        moveSpd = 6;
        agroDist = 15f;
        atkDist = 2f;
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<MeleeAttack>());
    }



    private void SetMovementTypes()
    {
        possibleMovements.Add(gameObject.AddComponent<NormalMovement>());
        possibleMovements.Add(gameObject.AddComponent<RunAway>());

    }

    public override void Move(int moveType, int enemy)
    {

        possibleMovements[moveType].ExecuteMovementPattern(agros[enemy].gameObject, .5f, agroDist);
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
                {
                    Attack(0);
                    if (!stoleItem)
                    {
                        Player target;
                        if (agros[i].TryGetComponent<Player>(out target))
                        {
                            target.stealFrom();
                            stoleItem = true;
                            isMoving = false;
                            StartCoroutine(RunAway());
                        }
                        
                    }
                }
                    


            }
        }

    }

    protected override void CheckMove()
    {
        for (int i = 0; i < agros.Length; i++)
        {

            if (agros[i] != null && !isMoving && !stoleItem)
            {
                if (Vector3.Distance(agros[i].transform.position, this.transform.position) < agroDist)
                {
                    isMoving = true;
                    Move(0, i);
                }

            }
            else if (agros[i] != null && !isMoving && stoleItem)
            {
                if (Vector3.Distance(agros[i].transform.position, this.transform.position) < agroDist)
                {
                    isMoving = true;
                    Move(1, i);
                }

            }
        }

    }

    private IEnumerator RunAway()
    {
        yield return new WaitForSeconds(runTime);
        Destroy(this.gameObject);
    }

    public override void TakeDamage(int damage, AttackType mytype)
    {
        if (mytype != AttackType.Magic)
        {
            health -= damage;
            if (health <= 0) OnDeath();
        }
        
    }

    public override void OnDeath()
    {
        Instantiate(_myDrops[0], transform.position + Vector3.right, transform.rotation);
        if (stoleItem)
        {
            GameObject statPotion = Instantiate(_myDrops[1], transform.position - Vector3.right, transform.rotation);
            statPotion.GetComponent<StatUpgrade>().myType = upgradeType;
        }
        
        base.OnDeath();
    }
}
