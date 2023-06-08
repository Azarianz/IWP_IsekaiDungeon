using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace IsekaiDungeon
{
    public class TestUI : MonoBehaviour
    {
        public TextMeshProUGUI gameStateText;
        public GameObject StartBtn;

        public GameObject gameCanvas, loseCanvas, winCanvas;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            gameStateText.text = GameManager.Instance.State.ToString();

            if (GameManager.Instance.State == GameState.GameStart)
            {
                StartBtn.SetActive(false);
            }
            if (GameManager.Instance.State == GameState.Lose)
            {
                gameCanvas.SetActive(false);
                loseCanvas.SetActive(true);
            }
            else if (GameManager.Instance.State == GameState.Win)
            {
                gameCanvas.SetActive(false);
                winCanvas.SetActive(true);
            }

            //Debug Only: Restart Scene
            if (Input.GetKey(KeyCode.R))
            {
                int activeScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(activeScene);
                Debug.Log("R Key Pressed");
            }
        }

        public void ChangeScene(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
