using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSwitch : MonoBehaviour
{
    public GameObject myPair;

    public void Activate()
    {
        myPair.gameObject.GetComponent<ActivateableObject>().Activate();
        Destroy(this.gameObject);
    }
}
