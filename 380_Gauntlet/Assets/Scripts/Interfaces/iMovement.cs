
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iMovement
{
    void ExecuteMovementPattern( GameObject targetLoc, float minDist, float maxDist);
}
