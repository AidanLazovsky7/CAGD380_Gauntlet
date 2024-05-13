using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public CharacterClass[] _characterData = new CharacterClass[4];
    private bool[] _availableCharacters = {true, true, true, true};
    private List<Player> _activePlayers = new List<Player>();
    public CharacterUI[] uis = new CharacterUI[4];
    public CameraController _cc;
    public GameObject cursor;
    private RectTransform cursorR;
    private TMP_Text cursorT;

    //get these refs only once
    private void Awake()
    {
        cursorR = cursor.GetComponent<RectTransform>();
        cursorT = cursor.GetComponent<TMP_Text>();
    }

    public bool isAvailable(int charNum)
    {
        return _availableCharacters[charNum];
    }

    public CharacterClass selectCharacter(int charNum, Player thePlayer)
    {
        //become my child, so that you can be not destroyed on load
        thePlayer.gameObject.transform.parent = this.gameObject.transform;
        _activePlayers.Add(thePlayer);
        _availableCharacters[charNum] = false;
        //this sets the UI as the observer of its player, so the player can notify the UI
        thePlayer.GetObserver(uis[charNum]);
        //this tells the camera that there's a new player
        _cc.setPlayers(_activePlayers);
        return _characterData[charNum];
    }

    //these functions allow characters to see the cursor move when they select a character
    //there's probably a better way to do this, but it's important to the game so here it is
    public void enableCursor()
    {
        cursor.SetActive(true);
    }

    public void moveCursor(int selection)
    {
        switch(selection)
        {
            case 0:
                cursorR.anchoredPosition = new Vector3(-260f, 460f, 0f);
                cursorT.color = new Color32(255, 34, 0, 255);
                break;
            case 1:
                cursorR.anchoredPosition = new Vector3(-260f, 210f, 0f);
                cursorT.color = new Color32(0, 34, 255, 255);
                break;
            case 2:
                cursorR.anchoredPosition = new Vector3(-260f, -90f, 0f);
                cursorT.color = new Color32(255, 255, 34, 255);
                break;
            case 3:
                cursorR.anchoredPosition = new Vector3(-260f, -340f, 0f);
                cursorT.color = new Color32(34, 255, 0, 255);
                break;
            default:
                Debug.Log("well that's not good");
                break;
        }
    }

    public void disableCursor()
    {
        cursor.SetActive(false);
    }
}
