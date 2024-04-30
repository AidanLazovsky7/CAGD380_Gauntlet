using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject _closestTeleporter;

    private bool _turnedOn = true;

    public void findPair(GameObject[] teleporters)
    {
        _closestTeleporter = teleporters[0];
        if (_closestTeleporter == this.gameObject)
        {
            _closestTeleporter = teleporters[1];
        }
        float closestDist = Vector3.Distance(_closestTeleporter.transform.position, this.transform.position);
        foreach (GameObject teleporter in teleporters)
        {
                float tryDist = Vector3.Distance(teleporter.transform.position, this.transform.position);
                if (tryDist < closestDist && teleporter != this.gameObject)
                {
                    _closestTeleporter = teleporter;
                    closestDist = tryDist;
                }
        }
    }
    public void Teleport(GameObject client)
    {
        if (_turnedOn)
        {
            _closestTeleporter.gameObject.GetComponent<Teleporter>().startCooldown();
            client.transform.position = new Vector3(_closestTeleporter.transform.position.x, _closestTeleporter.transform.position.y + 1, _closestTeleporter.transform.position.z);
        }
    }

    public void startCooldown()
    {
        StartCoroutine(cooldown());
    }

    private IEnumerator cooldown()
    {
        _turnedOn = false;
        yield return new WaitForSeconds(0.1f);
        _turnedOn = true;
    }
}
