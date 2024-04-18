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


    protected virtual void Awake()
    {
        if(!GetComponent<BoxCollider>())
        this.AddComponent<BoxCollider>();
        this.GetComponent<BoxCollider>().size = new Vector3(spawnRadius, 1, spawnRadius);
        this.GetComponent<BoxCollider>().isTrigger = true;
        //_gameManager = GamemMnager.Game;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set all values above here
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (CanSpawn())
            {
                GameObject enemy = Instantiate(enemyStates[currentLevel]);
                Vector3 randPos =Random.insideUnitSphere * spawnRadius/2;
                randPos.y = this.transform.position.y;
                enemy.transform.position = randPos;
                yield return new WaitForSeconds(spawnRate);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        OnDeath();
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

    private void OnTriggerEnter(Collider other)
    {
        iEnemy temp;
        if (other.gameObject.TryGetComponent<iEnemy>(out temp))
        {
            _nearbySpawns++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("something left");
        iEnemy temp;
        other.gameObject.TryGetComponent<iEnemy>(out temp);
        if (temp != null)
        {
            print("something should have happened");
            _nearbySpawns--;
            if (_nearbySpawns < 0) _nearbySpawns = 0;
        }
        else print("temp is null");
        print(" did something happen");
    }
}
