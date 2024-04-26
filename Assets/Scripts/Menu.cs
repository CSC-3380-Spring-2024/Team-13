using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void LevelSelect() {
        SceneHistory.LoadScene("Level Select");
    }

    public void Continue(){
        SceneHistory.loadLevelScene();
    }

    public void Level1(){
        SceneHistory.setLevelScene("Level 1");
        SceneManager.LoadScene("Level 1");
    }

    public void Quit() {
        Application.Quit();
        Debug.Log("player has quit the game");
    }
    
}
