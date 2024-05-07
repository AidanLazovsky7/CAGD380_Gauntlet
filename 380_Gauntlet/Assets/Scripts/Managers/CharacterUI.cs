using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : Observer
{
    public TMP_Text myscore;
    public TMP_Text myhealth;
    public Player myPlayer;

    public void UpdateMe()
    {
        SetScore();
        SetHealth();
    }

    private void SetScore()
    {
        myscore.text = myPlayer.getScore().ToString();
    }

    private void SetHealth()
    {
        myhealth.text = myPlayer.getHealth().ToString();
    }

    public void playerReference(Player player)
    {
        myPlayer = player;
    }

    //what do we do when the player notifies us?
    public override void Notify(Subject subject)
    {
        UpdateMe();
    }
}
