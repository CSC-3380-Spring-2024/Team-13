using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayCredits()
    {
        SceneManager.LoadScene("Credits");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
    {
        SceneManager.LoadScene("MenuScene");
        
    }        
    }
}
