using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //the camera needs an updated list of players when a player joins or leaves
    //the camera needs to calculate the midpoint of the players
    //it should move itself to the midpoint, += up and out
    //it should zoom further out the further apart the players are

    private List<Player> _players = new List<Player>();

    //how far from the point to be
    private Vector3 _offset = new Vector3(0f, 1f, -1.5f);

    public void setPlayers(List<Player> players)
    {
        _players = players;
    }

    private Vector3 midPoint()
    {
        //default to zero
        Vector3 mPoint = Vector3.zero;
        foreach (Player player in _players)
        {
            if (player == _players[0])
                mPoint = player.gameObject.transform.position;
            else
                mPoint = Vector3.Lerp(mPoint, player.transform.position, 0.5f);
        }
        return mPoint;
    }

    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, midPoint(), 0.1f) + _offset;
    }
}
