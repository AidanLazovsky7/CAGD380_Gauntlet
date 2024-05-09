using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : Observer
{
    public TMP_Text myscore;
    public TMP_Text myhealth;
    public TMP_Text mykeys;
    public TMP_Text mypotions;

    //what do we do when the player notifies us?
    public override void Notify(Subject subject)
    {
        Player thePlayer = (Player) subject;
        //all this is for updating the ui
        myscore.text = thePlayer.getScore().ToString();
        myhealth.text = thePlayer.getHealth().ToString();
        mykeys.text = thePlayer.myKeys().ToString();
        mypotions.text = thePlayer.myPotions().ToString();
    }
}
