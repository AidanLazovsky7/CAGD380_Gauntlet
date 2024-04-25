using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, iCollectable
{
    public void pickup(Player user)
    {
        user.getPotion();
        Destroy(this.gameObject);
    }
}
