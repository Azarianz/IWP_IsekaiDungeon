using IsekaiDungeon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Town : MonoBehaviour
{
    public GameObject[] all_canvas;
    public GameObject[] popup_menus;
    public GameObject[] town_menus;
    public GameObject dungeon_canvas, town_canvas;
    public Slider towerSlider;
    public Animator notificationMessage;

    public TextMeshProUGUI dungeonFloor, notificationMessageTxt;

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

    public void toggle_AllLayout(bool condition)
    {
        for (int i = 0; i < town_menus.Length; i++)
        {
            all_canvas[i].SetActive(condition);
        }
    }

    public void ShowDungeonMap()
    {
        town_canvas.SetActive(false);
        dungeon_canvas.SetActive(true);
        towerSlider.maxValue = 100;
        towerSlider.value = PlayerManager.instance.PROGRESSION_DUNGEON_CURRENTFLOOR;
        dungeonFloor.text = "Floor " + (PlayerManager.instance.PROGRESSION_DUNGEON_CURRENTFLOOR + 1);
    }

    public void OnPressedEnterDungeon()
    {
        int currentFloor = PlayerManager.instance.PROGRESSION_DUNGEON_CURRENTFLOOR;

        if(currentFloor >= 11)
        {
            notificationMessageTxt.text = "End Demo";
            notificationMessage.GetComponent<Animator>().Play("NotificationMessage");
        }

        else if (InventoryController.Inventory_Instance.AddStamina(-2))
        {
            SceneManager.LoadScene("Game_Dungeon");
        }
        else
        {
            notificationMessageTxt.text = "Out of stamina!"; ;
            notificationMessage.GetComponent<Animator>().Play("NotificationMessage");
        }
    }

    public void OnPressedQuitDungeon()
    {
        town_canvas.SetActive(true);
        dungeon_canvas.SetActive(false);
    }

    public void NextDay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
