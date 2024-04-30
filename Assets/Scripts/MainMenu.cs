using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void Play() 
    {
        LoadNextLevel();
    }

    public void Quit() 
    {
        Application.Quit();
        Debug.Log("player has quit the game");
    }

    public void LoadNextLevel() 
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //Load Scene
        SceneManager.LoadScene("topdownscene");
    }
}
