using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GeneratorParent : MonoBehaviour, iDamageable
{


    [Tooltip("These are the prefabs for the different states the generator can be in")]
    [SerializeField]
    protected List<GameObject> _generatorStates = new List<GameObject>();

    [Tooltip("These are the prefabs for the different states the enemys can be in")]
    [SerializeField]
    protected List<GameObject> _enemyStates = new List<GameObject>();

    [Tooltip("Set the starting level for the generator here")]
    [SerializeField]
    protected int _currentLevel;

    [SerializeField]
    protected int _spawnRate;

    [Tooltip("SET BEFORE PLACING IN SCENE, a float to depict how large the spawn radius is")]
    [SerializeField]
    protected float spawnRadius;

    private float _nearbySpawns;


    protected virtual void Awake()
    {
        this.AddComponent<SphereCollider>();
        this.GetComponent<SphereCollider>().radius = spawnRadius;
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
                Instantiate(_enemyStates[_currentLevel]);
                yield return new WaitForSeconds(_spawnRate);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        OnDeath();
    }

    public void OnDeath()
    {
        _currentLevel--;
        if (_currentLevel >= 0) Instantiate(_generatorStates[_currentLevel], transform.position, transform.rotation);
        Destroy(this.gameObject);
    }


    private bool CanSpawn()
    {
        if (_nearbySpawns >= 8) return false;
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyParent>())
        {
            _nearbySpawns++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyParent>())
        {
            _nearbySpawns--;
            if (_nearbySpawns < 0) _nearbySpawns = 0;
        }
    }
}
