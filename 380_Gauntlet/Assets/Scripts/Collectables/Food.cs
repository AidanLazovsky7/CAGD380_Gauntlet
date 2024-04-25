using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, iCollectable
{
    private int _healing = 100;

    public void pickup(Player user)
    {
        user.addHealth(_healing);
        Destroy(this.gameObject);
    }
}
