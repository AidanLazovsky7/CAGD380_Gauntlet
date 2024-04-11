using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterClass[] _characterData = new CharacterClass[4];
    private bool[] _availableCharacters = {true, true, true, true};

    public bool isAvailable(int charNum)
    {
        return _availableCharacters[charNum];
    }

    public CharacterClass selectCharacter(int charNum)
    {
        _availableCharacters[charNum] = false;
        return _characterData[charNum];
    }
}
