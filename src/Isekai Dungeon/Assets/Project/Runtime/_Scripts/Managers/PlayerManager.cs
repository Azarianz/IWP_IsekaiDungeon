using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsekaiDungeon
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        public int PROGRESSION_DUNGEON_CURRENTFLOOR = 0;

        private void Start()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}
