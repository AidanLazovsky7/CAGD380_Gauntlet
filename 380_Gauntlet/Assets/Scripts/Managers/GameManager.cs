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

    //at the start of a new level, tell the teleporters to connect
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
        else if (teleporters.Length == 1)
        {
            Debug.Log("Designer! There needs to be more than one teleporter!");
        }
    }

    //this runs when we go to a new level
    public void newLevel()
    {
        Debug.Log("setting up new level");
        StartCoroutine(waitAndCheck());
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

    private IEnumerator waitAndCheck()
    {
        yield return new WaitForSeconds(0.5f);
        activateTeleporters();
    }
}
