using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : MonoBehaviour, iCollectable
{
    public UpgradeType myType;

    public void pickup(Player user)
    {
        user.getUpgrade(myType);
        Destroy(this.gameObject);
    }
}
