using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour, iDamageable
{
    [Tooltip("These are the prefabs for the different states the Wall can be in")]
    [SerializeField]
    private List<GameObject> _wallStates = new List<GameObject>();

    private int _currentLevel;

    public void TakeDamage(float damage)
    {
        OnDeath();
    }

    public void OnDeath()
    {
        _currentLevel--;
        if (_currentLevel >= 0) Instantiate(_wallStates[_currentLevel], transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
