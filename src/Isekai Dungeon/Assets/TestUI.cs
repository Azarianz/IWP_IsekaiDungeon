using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IsekaiDungeon
{
    public class TestUI : MonoBehaviour
    {
        public TextMeshProUGUI gameStateText;
        public GameObject StartBtn;

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
        }
    }
}
