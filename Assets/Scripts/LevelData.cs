using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    public TextMeshProUGUI[] defeats;

    public static LevelData levelmanager;

    void Awake()
    {
        // Ensure that level 1 is unlocked by default
        highestlevel = Math.Max(PlayerPrefs.GetInt("highestLevel", 1), 1);
        currentlevel = PlayerPrefs.GetInt("currentLevel", 1);

        lvl1count = PlayerPrefs.GetInt("count1", 0);
        lvl2count = PlayerPrefs.GetInt("count2", 0);

        lvl1bool = PlayerPrefs.GetInt("bool1", 0);
        lvl2bool = PlayerPrefs.GetInt("bool2", 0);
        lvl3bool = PlayerPrefs.GetInt("bool3", 0);
        lvl4bool = PlayerPrefs.GetInt("bool4", 0);
        lvl5bool = PlayerPrefs.GetInt("bool5", 0);

        updateLevels();
    }

    //Called when level select scene is loaded
    void OnEnable()
    {
        Debug.Log("Enabled!");
        updateLevels();
    }

    //Updates level select images (whether button is interactable, lock img, check img)
    public void updateLevels()
    {
        highestlevel = PlayerPrefs.GetInt("highestLevel");
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int lvlnum = i + 1;

            if (lvlnum == 1 && PlayerPrefs.GetInt("count" + lvlnum) >= 3)
            {
                // Unlock level 2
                highestlevel = Math.Max(highestlevel, 2);
                PlayerPrefs.SetInt("highestLevel", highestlevel);
                if (defeats.Length > i)
                {
                    defeats[i].enabled = false;
                }
            }

            //Locks level if its past highestlevel unlocked
            if (lvlnum > highestlevel)
            {
                levelButtons[i].enabled = false;
                lockimgs[i].enabled = true;
                checkimgs[i].enabled = false;
            }
            //Determines if level is completed 
            else if (PlayerPrefs.GetInt("bool" + lvlnum) == 1)
            {
                lockimgs[i].enabled = false;
                checkimgs[i].enabled = true;
                levelButtons[i].enabled = false;
            }
            //Level is unlocked and not complete
            else
            {
                lockimgs[i].enabled = false;
                levelButtons[i].enabled = true;
                checkimgs[i].enabled = false;
                defeats[i].enabled = true;

                //Updates the enemydefeats count for each level
                switch (lvlnum)
                {
                    case 1:
                        defeats[i].text = (PlayerPrefs.GetInt("count" + lvlnum)) + "/3";
                        break;
                    case 2:
                        defeats[i].text = (PlayerPrefs.GetInt("count" + lvlnum)) + "/1";
                        break;
                }
            }
        }
    }

    //Used for level select buttons
    public void loadLevelScene(int num)
    {
        PlayerPrefs.SetInt("currentLevel", num);
        SceneManager.LoadScene("Level " + num);
    }

    //Resets player data
    public static void Reset()
    {

        PlayerPrefs.SetInt("highestLevel", 1);
        PlayerPrefs.SetInt("currentLevel", 1);

        PlayerPrefs.SetInt("count1", 0);
        PlayerPrefs.SetInt("count2", 0);

        PlayerPrefs.SetInt("bool1", 0);
        PlayerPrefs.SetInt("bool2", 0);
        PlayerPrefs.SetInt("bool3", 0);
        PlayerPrefs.SetInt("bool4", 0);
        PlayerPrefs.SetInt("bool5", 0);

        PlayerPrefs.Save();

        Debug.Log("Player progress reset");
    }
}
