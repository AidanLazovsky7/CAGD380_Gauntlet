using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour, iDamageable
{
    [Tooltip("These are the prefabs for the different states the Wall can be in, there are a max of 3")]
    [SerializeField]
    private List<GameObject> _wallStates = new List<GameObject>();

    [Tooltip("Set how many hits you want the wall to take here, it should be less than the amount of wall states")]
    [Range(0,2)]
    [SerializeField]
    private int _currentLevel = -1;

    private GameObject _currentWall;

    private void Start()
    {
        if (_currentLevel == -1) _currentLevel = _wallStates.Count - 1;
       
        
       _currentWall = Instantiate(_wallStates[_currentLevel], transform.position, transform.rotation);
    }

    public void TakeDamage(int damage, AttackType atkType)
    {
        OnDeath();
    }

    public void OnDeath()
    {
        _currentLevel--;
        GameObject temp = _currentWall;
        if (_currentLevel >= 0)
        {
            _currentWall = Instantiate(_wallStates[_currentLevel], transform.position, transform.rotation);
        }
        Destroy(temp);
        this.gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Hit Wall"))
        {
            TakeDamage(1, AttackType.Missile);
        }
    }
}
