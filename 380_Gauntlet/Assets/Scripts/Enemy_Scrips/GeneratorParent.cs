using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GeneratorParent : MonoBehaviour, iDamageable
{


    [Tooltip("These are the prefabs for the different states the generator can be in")]
    [SerializeField]
    protected List<GameObject> generatorStates = new List<GameObject>();

    [Tooltip("These are the prefabs for the different states the enemys can be in")]
    [SerializeField]
    protected List<GameObject> enemyStates = new List<GameObject>();

    [Tooltip("Set the starting level for the generator here")]
    [SerializeField]
    protected int currentLevel;

    [SerializeField]
    protected int spawnRate;

    [Tooltip("SET BEFORE PLACING IN SCENE, a float to depict how large the spawn radius is")]
    [SerializeField]
    protected float spawnRadius;

    [SerializeField]
    private float _nearbySpawns;

    protected int currentHealth = 2;

    [SerializeField] LayerMask enemyMask;

    public Collider[] NearbySpawns;


    // Start is called before the first frame update
    void Start()
    {
        NearbySpawns = new Collider[9];
        //Set all values above here
        StartCoroutine(SpawnEnemy());
    }

    private void FixedUpdate()
    {
        _nearbySpawns = Physics.OverlapSphereNonAlloc(transform.position, spawnRadius, NearbySpawns, enemyMask, QueryTriggerInteraction.Ignore);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (CanSpawn())
            {
                GameObject enemy = Instantiate(enemyStates[currentLevel]);
                Vector3 randPos =Random.insideUnitSphere * spawnRadius/2;
                randPos.y = 0;
                enemy.transform.position = randPos + transform.position;
                yield return new WaitForSeconds(spawnRate);
            }
            else
            {
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    public void TakeDamage(int damage, AttackType atkType)
    {
        currentHealth -= damage;
        if(currentHealth <=0) OnDeath();

    }

    public void OnDeath()
    {
        currentLevel--;
        if (currentLevel >= 0) Instantiate(generatorStates[currentLevel], transform.position, transform.rotation);
        Destroy(this.gameObject);
    }


    private bool CanSpawn()
    {
        if (_nearbySpawns >= 8) return false;
        return true;
    }

   
}
