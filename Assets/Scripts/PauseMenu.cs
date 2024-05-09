using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour 
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;

    //Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PauseMenuCanvas.SetActive(false);
        
    }

    //Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
    {
        if(Paused)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }        
    }
    //Pause game in the background
    void Stop()
    {
        PauseMenuCanvas.SetActive(true); 
            Time.timeScale = 0f;
        Paused = true;
    }
    //Resume game
    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        Paused = false;
    }

    //Used for "Main Menu" button
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
