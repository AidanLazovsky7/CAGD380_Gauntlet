using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, iCollectable
{
    [SerializeField]
    [Range(100,500)]
    private int _value;

    public void pickup(Player user)
    {
        user.addScore(_value);
        Destroy(this.gameObject);
    }
}
