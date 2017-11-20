using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_machines : MonoBehaviour {

    // Use this for initialization
    public void LoadON()
    {
        //załaduje level 1 numer odpowiada Build settings numer
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void quit()
    {
        Application.Quit();
    }

    public void LoadONI()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
