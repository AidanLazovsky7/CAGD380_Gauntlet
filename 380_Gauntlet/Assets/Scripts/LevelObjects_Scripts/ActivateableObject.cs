using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateableObject : MonoBehaviour
{
    public void Activate()
    {
        Destroy(this.gameObject);
    }
}
