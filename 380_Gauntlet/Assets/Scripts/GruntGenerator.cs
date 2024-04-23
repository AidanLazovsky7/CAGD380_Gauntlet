using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntGenerator : GeneratorParent
{
    private void OnGUI()
    {
        if (GUILayout.Button("Hit Generator"))
        {
            TakeDamage(1);
        }
    }
}
