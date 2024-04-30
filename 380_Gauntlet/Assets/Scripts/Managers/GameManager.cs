using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        activateTeleporters();
    }

    private void activateTeleporters()
    {
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
        if (teleporters.Length > 1)
        {
            foreach (GameObject teleporter in teleporters)
            {
                teleporter.gameObject.GetComponent<Teleporter>().findPair(teleporters);
            }
        }
        else
        {
            Debug.Log("Designer! There needs to be more than one teleporter!");
        }
    }
}
