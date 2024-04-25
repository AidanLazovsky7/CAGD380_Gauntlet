using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, iCollectable
{
    public void pickup(Player user)
    {
        user.getKey();
        Destroy(this.gameObject);
    }
}
