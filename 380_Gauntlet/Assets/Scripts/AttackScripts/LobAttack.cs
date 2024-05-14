using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobAttack : MonoBehaviour, iAttack
{
    private EnemyParent _myEnemy;

    private GameObject _myProjectile;

    public GameObject _myTarget;

    private void Awake()
    {
        _myEnemy = this.GetComponent<EnemyParent>() as EnemyParent;
        _myProjectile = _myEnemy.projectile;
    }

    public void ExecuteAttackPattern(float atkspd, float atkdur)
    {
        StartCoroutine(LobProjectile(atkspd, atkdur));
    }

    private IEnumerator LobProjectile(float atkspd, float atkdur)
    {
        _myEnemy.isAttacking = true;
        //windup
        yield return new WaitForSeconds(atkspd);
        //the attack itself
        GameObject thrownProj = Instantiate(_myProjectile, _myEnemy.transform.position, _myEnemy.transform.rotation);
        thrownProj.gameObject.GetComponent<LobbedProjectile>().lob(_myTarget);
        //winddown
        yield return new WaitForSeconds(atkdur);
        _myEnemy.isAttacking = false;
    }
}
