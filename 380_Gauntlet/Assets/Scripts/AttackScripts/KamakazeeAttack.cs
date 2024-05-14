using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamakazeeAttack : MonoBehaviour, iAttack
{
    private EnemyParent _myEnemy;

    private void Awake()
    {
        _myEnemy = this.GetComponent<EnemyParent>() as EnemyParent;
    }

    public void ExecuteAttackPattern(float atkspd, float atkdur)
    {
        StartCoroutine(Kamakazee());
    }

    private IEnumerator Kamakazee()
    {
        
            _myEnemy.GetComponent<GhostScript>().KamakazeeNow();
        print("kaboom");
            yield return null;
        
        
    }
}
