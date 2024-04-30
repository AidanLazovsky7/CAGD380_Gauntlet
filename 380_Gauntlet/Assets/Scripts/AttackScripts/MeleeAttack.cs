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

    public void ExecuteAttackPattern(float atkspd, float atkdur)
    {
        StartCoroutine(Melee(atkspd, atkdur));
    }

    private IEnumerator Melee(float atkspd, float atkdur)
    {
        _myEnemy.isAttacking = true;
        Debug.Log("I am in attack start up");
        yield return new WaitForSeconds(atkspd); 
        _myEnemy.isDamaging = true;
        Debug.Log("I am attacking");
        yield return new WaitForSeconds(atkdur);
        _myEnemy.isAttacking = false;
        _myEnemy.isDamaging = false;
        Debug.Log("My Attack is over");
    }
}
