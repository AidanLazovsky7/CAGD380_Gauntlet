using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, iAttack 
{

    private EnemyParent _myEnemy;

    private void Awake()
    {
        _myEnemy = this.GetComponent<EnemyParent>() as EnemyParent;
    }

    public void AttackPattern(float atkspd, float atkdur)
    {
        StartCoroutine(Melee(atkspd, atkdur));
    }

    private IEnumerator Melee(float atkspd, float atkdur)
    {
        yield return new WaitForSeconds(atkspd);
        _myEnemy.isAttacking = true;
        yield return new WaitForSeconds(atkdur);
        _myEnemy.isAttacking = false;
    }
}
