using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Main : MonoBehaviour
{
    public List<Canvas> canvasList;

    //QUIT BUTTON
    public void OnPressedQuit()
    {
        Application.Quit();
    }

    public void DisplayCanvas(Canvas canvs)
    {
        foreach (Canvas c in canvasList)
        {
            if(c == canvs)
            {
                c.enabled = true;
            }
            else
            {
                c.enabled = false;
            }
        }
    }

    public void ToggleGameobject(GameObject go)
    {
        bool isActive = go.activeSelf;
        go.SetActive(!isActive);
    }
}
