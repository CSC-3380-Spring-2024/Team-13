using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Used for "Play" button
    public void LevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

    //Determines whether the "Continue" button completes the level or takes the player back into it
    public void Continue(){
        int lvl=PlayerPrefs.GetInt("currentLevel");
        switch(lvl){
            case 1:
            if (PlayerPrefs.GetInt("count1")==3) //Must defeat 3 enemies to complete level 1
            { 
                PlayerPrefs.SetInt("bool1",1);
                PlayerPrefs.SetInt("highestLevel",PlayerPrefs.GetInt("highestLevel")+1);
                SceneManager.LoadScene("LevelSelect");
            }
            else{
                SceneManager.LoadScene("Level "+lvl);
            }
            break;
            case 2:
            if (PlayerPrefs.GetInt("count2")==1) //Must defeat 1 enemy to complete level 2
            {
                SceneManager.LoadScene("LevelSelect");
                PlayerPrefs.SetInt("bool2",1);
            }
            else{
                SceneManager.LoadScene("Level "+lvl);
            }
            break;
        }
    }

    //Used for buttons that load main menu
    public void Mainmenu(){
        SceneManager.LoadScene("MenuScene");
    }

    //Used for "Quit" button
    public void Quit() {
        LevelData.Reset();
        Application.Quit();
        Debug.Log("player has quit the game");
    }   
}
