using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Battle : MonoBehaviour
{
    public void OnRetreatBattle()
    {
        SceneManager.LoadScene("Game_Town");
    }
}
