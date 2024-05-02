using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : MonoBehaviour
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
}
