using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterClass[] _characterData = new CharacterClass[4];
    private bool[] _availableCharacters = {true, true, true, true};
    [SerializeField]
    private List<Player> _activePlayers = new List<Player>();

    public bool isAvailable(int charNum)
    {
        return _availableCharacters[charNum];
    }

    public CharacterClass selectCharacter(int charNum, Player thePlayer)
    {
        _activePlayers.Add(thePlayer);
        _availableCharacters[charNum] = false;
        return _characterData[charNum];
    }
}
