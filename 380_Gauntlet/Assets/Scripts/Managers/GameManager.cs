using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> visibleEnemies = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();
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

    //find all visible enemies


    //players call this function to damage enemies with a potion
    public void usePotion(int damage)
    {
        foreach(GameObject enemy in visibleEnemies)
        {
            enemy.gameObject.GetComponent<iDamageable>().TakeDamage(damage, AttackType.Magic);
        }
    }
}
