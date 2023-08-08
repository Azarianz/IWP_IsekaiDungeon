using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsekaiDungeon
{
    public class FlashDebug : MonoBehaviour
    {
        [SerializeField] private SimpleFlash flashEffect { get { return GetComponent<SimpleFlash>(); } }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyUp(KeyCode.V))
            {
                flashEffect.Flash();
                Debug.Log("Flash");
            }

        }
    }
}
