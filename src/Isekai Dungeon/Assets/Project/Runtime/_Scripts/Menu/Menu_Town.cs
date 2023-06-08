using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Town : MonoBehaviour
{
    public GameObject[] popup_menus;
    public GameObject[] town_menus;

    public void show_popup(GameObject popup)
    {
        for(int i = 0; i < popup_menus.Length; i++)
        {
            if (popup_menus[i] == popup)
            {
                popup_menus[i].SetActive(true);
            }
            else
            {
                popup_menus[i].SetActive(false);
            }
        }
    }

    public void hideall_popups()
    {
        for (int i = 0; i < popup_menus.Length; i++)
        {
            popup_menus[i].SetActive(false);
        }
    }

    public void show_defaultTownLayout()
    {
        for (int i = 0; i < town_menus.Length; i++)
        {
            town_menus[i].SetActive(true);
        }
    }

    public void hide_defaultTownLayout()
    {
        for (int i = 0; i < town_menus.Length; i++)
        {
            town_menus[i].SetActive(false);
        }
    }

    public void OnPressedEnterDungeon()
    {
        SceneManager.LoadScene("Game_Dungeon");
    }

}
