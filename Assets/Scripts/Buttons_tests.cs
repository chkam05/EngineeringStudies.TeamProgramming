using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_tests : MonoBehaviour {

    // Use this for initialization
    public void LoadONW()
    {
        //załaduje level 1 numer odpowiada Build settings numer
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadON1()
    {
        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }

    public void LoadON2()
    {
        SceneManager.LoadScene(8, LoadSceneMode.Single);
    }

    public void LoadON3()
    {
        SceneManager.LoadScene(9, LoadSceneMode.Single);
    }

    public void LoadON4()
    {
        SceneManager.LoadScene(10, LoadSceneMode.Single);
    }
}
