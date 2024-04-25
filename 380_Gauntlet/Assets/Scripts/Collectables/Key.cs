using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Key : MonoBehaviour, iCollectable
{
    private int _value = 100;
    public void pickup(Player user)
    {
        user.getKey();
        user.addScore(_value);
        Destroy(this.gameObject);
    }
}
