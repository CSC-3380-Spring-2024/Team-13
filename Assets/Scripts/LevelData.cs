using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelData : MonoBehaviour
{
    public static string levelscene;

    public int highestlevel, currentlevel;

    private int lvl1count, lvl2count;

    private int lvl1bool, lvl2bool, lvl3bool, lvl4bool, lvl5bool;

    public Button[] levelButtons;

    public Image[] lockimgs;
    public Image[] checkimgs;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        highestlevel = PlayerPrefs.GetInt("highestLevel",1);
        currentlevel = PlayerPrefs.GetInt("currentLevel",1);
        lvl1count = PlayerPrefs.GetInt("count1",0);
        lvl2count = PlayerPrefs.GetInt("count2",0);

        lvl1bool = PlayerPrefs.GetInt("bool1",0);
        lvl2bool = PlayerPrefs.GetInt("bool2",0);
        lvl3bool = PlayerPrefs.GetInt("bool3",0);
        lvl4bool = PlayerPrefs.GetInt("bool4",0);
        lvl5bool = PlayerPrefs.GetInt("bool5",0);

        updateLevels();
    }

    //Called when level select scene is loaded
    void OnEnable(){
        Debug.Log("Enabled!");
        updateLevels();
    }

    //Updates level select images (whether button is interactable, lock img, check img)
    public void updateLevels(){
        highestlevel = PlayerPrefs.GetInt("highestLevel");
        for (int i = 0;i<levelButtons.Length;i++){
            int lvlnum = i+1;
            
            //Locks all levels that are after player's highest level
            if (lvlnum>highestlevel){
                levelButtons[i].enabled = false;
                lockimgs[i].enabled=true;
                checkimgs[i].enabled=false;
            }
            else{
                lockimgs[i].enabled = false;
                //Determines if level is completed 
                if (PlayerPrefs.GetInt("bool"+lvlnum)==1){
                    checkimgs[i].enabled=true;
                    levelButtons[i].enabled=false;
                }
                else{//If the statement gets to here, the level is unlocked and not complete
                    levelButtons[i].enabled=true;
                    checkimgs[i].enabled=false;
                }    
            }
        }
    }

    //Used for level select buttons
    public void loadLevelScene(int num){
        PlayerPrefs.SetInt("currentLevel",num);
        SceneManager.LoadScene("Level "+num);
    }


    //Resets player data
    public static void Reset(){
        
        PlayerPrefs.SetInt("highestLevel",1);
        PlayerPrefs.SetInt("currentLevel",1);

        PlayerPrefs.SetInt("count1",0);
        PlayerPrefs.SetInt("count2",0);

        PlayerPrefs.SetInt("bool1",0);
        PlayerPrefs.SetInt("bool2",0);
        PlayerPrefs.SetInt("bool3",0);
        PlayerPrefs.SetInt("bool4",0);
        PlayerPrefs.SetInt("bool5",0);

        PlayerPrefs.Save();

        Debug.Log("Player progress reset");
    }
}
