using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Main : MonoBehaviour
{
    //PLAY BUTTON
    public void OnPressedPlay()
    {
        SceneManager.LoadScene("Game_Town");
    }

    //QUIT BUTTON
    public void OnPressedQuit()
    {
        Application.Quit();
    }
}
