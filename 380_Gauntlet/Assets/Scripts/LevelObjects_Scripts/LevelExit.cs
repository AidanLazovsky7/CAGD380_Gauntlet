using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    List<GameObject> exitedPlayers = new List<GameObject>();

    //when a player touches me,  call this function--i'll add them to my list
    //then, they should deactivate themself
    //when i'm touched, i'll check if there's anyone elses in the level
    //if not, load the next one and re-enable the players.
    public void openExit(GameObject player)
    {
        exitedPlayers.Add(player);
        //if there's no other players, wait a second and then load the next level
        StartCoroutine(checkAndLoad());
    }

    private IEnumerator checkAndLoad()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            nextLevel();
        }
        else
        {
            Debug.Log("There's somebody else in the level");
        }
    }

    //load the next level, inform the gameManager, reactivate players, set them to 0,0
    private void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.newLevel();
        foreach (GameObject player in exitedPlayers)
        {
            player.transform.position = new Vector3(0f, 1.5f, 0f);
            player.SetActive(true);
        }
    }
}
