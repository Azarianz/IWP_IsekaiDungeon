using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DEBUGING : MonoBehaviour
{
    public GameObject Blue, Red;
    public Transform bSpawn, rSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(Blue, bSpawn.position, Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(Red, rSpawn.position, Quaternion.identity);
        }
    }
}
