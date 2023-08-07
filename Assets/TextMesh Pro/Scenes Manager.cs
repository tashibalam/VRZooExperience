using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    private void Awake()
    {
        instance = this;
    }
    public enum Scene
    {
        AnimalScene2,
        ParkScene
    }

    public void LoadScene(Scene scene1)
    {
        SceneManager.LoadScene(scene1.ToString());
    }

    public void LoadNewGame()
    { 
        SceneManager.LoadScene(Scene.ParkScene.ToString());
    }

    public void LoadNextScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadAnimalScene2()
    {
        SceneManager.LoadScene(Scene.AnimalScene2.ToString());
    }
}
