using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;


public class SceneSwitcher2 : MonoBehaviour
{
    /*  public static MenuManager Instance;

    void Awake()
    {
        Instance = this;
    }*/

    public void play_Game()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
    }
}
