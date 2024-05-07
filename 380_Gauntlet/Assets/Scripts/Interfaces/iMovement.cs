
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iMovement
{
    void ExecuteMovementPattern(float minDist, float maxDist, Vector3 targetLoc);
}
