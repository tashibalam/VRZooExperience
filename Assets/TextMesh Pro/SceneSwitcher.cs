using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
//using MenuManager;

public class SceneSwitcher : MonoBehaviour
{

  /*  public static MenuManager Instance;

    void Awake()
    {
        Instance = this;
    }*/

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    


}
