using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour, iAttack
{
    private EnemyParent _myEnemy;

    private GameObject _myProjectile;    
    

    private void Awake()
    {
        _myEnemy = this.GetComponent<EnemyParent>() as EnemyParent;
        _myProjectile = _myEnemy.projectile;
    }

    public void ExecuteAttackPattern(float atkspd, float atkdur)
    {
        StartCoroutine(FireBall(atkspd, atkdur));
    }

    private IEnumerator FireBall(float atkspd, float atkdur)
    {
        _myEnemy.isAttacking = true;
        Debug.Log("I am in attack start up");
        yield return new WaitForSeconds(atkspd);
        Instantiate(_myProjectile, _myEnemy.transform.position, _myEnemy.transform.rotation);
        Debug.Log("I am attacking");
        yield return new WaitForSeconds(atkdur);
        _myEnemy.isAttacking = false;
        Debug.Log("My Attack is over");
    }
}
