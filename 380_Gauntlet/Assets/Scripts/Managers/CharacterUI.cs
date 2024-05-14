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
    public TMP_Text myupgrades;

    //what do we do when the player notifies us?
    public override void Notify(Subject subject)
    {
        Player thePlayer = (Player) subject;
        //all this is for updating the ui
        myscore.text = thePlayer.getScore().ToString();
        myhealth.text = thePlayer.getHealth().ToString();
        mykeys.text = thePlayer.myKeys().ToString();
        mypotions.text = thePlayer.myPotions().ToString();
        //for the upgrades
        string display = "";
        foreach (UpgradeType upgrade in thePlayer.getUpgrades())
        {
            if (upgrade == UpgradeType.Armor)
                display += "<sprite=0>";
            else if (upgrade == UpgradeType.Magic)
                display += "<sprite=1>";
            else if (upgrade == UpgradeType.ShotSpeed)
                display += "<sprite=2>";
            else if (upgrade == UpgradeType.Speed)
                display += "<sprite=3>";
            else if (upgrade == UpgradeType.FightPower)
                display += "<sprite=4>";
        }
        myupgrades.text = display;
    }
}
