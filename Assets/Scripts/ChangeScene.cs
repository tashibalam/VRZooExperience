using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IPointerClickHandler
{
    public int sceneID;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneID);
    }
    private void OnMouseDown()
    {
        Debug.Log("Sprite clicked!");
        SceneManager.LoadScene(sceneID);
    }
}

