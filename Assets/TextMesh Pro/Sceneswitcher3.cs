using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;


public class Sceneswitcher3 : MonoBehaviour
{
    /*  public static MenuManager Instance;

    void Awake()
    {
        Instance = this;
    }*/

    public void play_game()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 1);
    }
}
