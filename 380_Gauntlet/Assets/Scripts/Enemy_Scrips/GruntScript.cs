using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : EnemyParent
{

    protected override void Awake()
    {
        base.Awake();
        score[0] = 25;
        score[1] = 5;
        score[2] = 10;
        score[3] = 10;
        atkSpd = .5f;
        atkDuration = .25f;
        moveSpd = 2;
        agroDist = 5f;
        atkDist = .5f;
    }

    public override IEnumerator Move()
    {
        yield return null;
    }

    public override IEnumerator Attack()
    {
        yield return null;
    }
}
