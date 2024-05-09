using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public CharacterClass[] _characterData = new CharacterClass[4];
    private bool[] _availableCharacters = {true, true, true, true};
    [SerializeField]
    private List<Player> _activePlayers = new List<Player>();
    public CharacterUI[] uis = new CharacterUI[4];
    public bool isAvailable(int charNum)
    {
        return _availableCharacters[charNum];
    }

    public CharacterClass selectCharacter(int charNum, Player thePlayer)
    {
        _activePlayers.Add(thePlayer);
        _availableCharacters[charNum] = false;
        //this sets the UI as the observer of its player, so the player can notify the UI
        thePlayer.GetObserver(uis[charNum]);
        return _characterData[charNum];
    }

    /*
    public void updateUI()
    {
        foreach (CharacterUI thisui in uis)
        {
            if(thisui.myPlayer != null)
                thisui.UpdateMe();
        }
    }*/
}
