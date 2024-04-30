using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void StartGame()
    {

        SceneManager.LoadScene("GameplayScene");

    }

    public void OpenOptions()
    {

        Debug.Log("Options Menu Opened");
    }

    public void QuitGame()
    {

        Application.Quit();
    }
}