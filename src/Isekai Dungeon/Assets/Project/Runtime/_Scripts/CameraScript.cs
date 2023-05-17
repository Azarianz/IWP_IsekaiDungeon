using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Vector3 targetDest = transform.position - transform.right;
            Vector3 lerpPosition = Vector3.Lerp(transform.position, targetDest, cameraSpeed);
            transform.position = lerpPosition;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Vector3 targetDest = transform.position + transform.right;
            Vector3 lerpPosition = Vector3.Lerp(transform.position, targetDest, cameraSpeed);
            transform.position = lerpPosition;
        }
    }
}
