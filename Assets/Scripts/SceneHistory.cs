using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHistory : MonoBehaviour
{
    public static string scene;

    void Start()
    {
       DontDestroyOnLoad(this.gameObject);
    }

    public static void LoadScene(string newScene){
        //sceneHistory.Add(newScene);
        //Debug.Log(newScene);
        SceneManager.LoadScene(newScene);
    }

    public static void setLevelScene(string newScene){
        scene=newScene;
    }

    public static void loadLevelScene(){
        SceneManager.LoadScene(scene);
    }
}
