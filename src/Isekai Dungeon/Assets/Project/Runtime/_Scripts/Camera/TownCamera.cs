using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsekaiDungeon
{
    public class TownCamera : MonoBehaviour
    {
        private Camera mainCamera;
        private Plane plane;

        public float rotationSpeed = 5f;
        public float zOffset = 0f; // Desired Z position offset

        public Vector2 clampRotationX; // Minimum and maximum X values for camera rotation
        public Vector2 clampRotationY; // Minimum and maximum Y values for camera rotation

        private Animator cam_anim { get { return GetComponent<Animator>(); } }
        public Menu_Town menuTownScript;

        void Start()
        {
            mainCamera = Camera.main;
            plane = new Plane(Vector3.forward, Vector3.zero); // Create a plane with normal facing upwards
            if (PlayerManager.instance.PROGRESSION_DUNGEON_CURRENTFLOOR >= 1)
            {
                cam_anim.Play("Return_Town");
            }
            else
            {
                cam_anim.Play("Enter_Town");
            }

        }

        void Update()
        {
            // Get the cursor position in screen space
            Vector3 cursorPosition = Input.mousePosition;

            // Cast a ray from the camera to the cursor position
            Ray ray = mainCamera.ScreenPointToRay(cursorPosition);

            float rayDistance = 10f;
            if (plane.Raycast(ray, out rayDistance))
            {
                // Get the point of intersection between the ray and the plane
                Vector3 hitPoint = ray.GetPoint(rayDistance);

                // Offset the Z position
                hitPoint += new Vector3(0f, 0f, zOffset);


                // Draw a debug line from the camera to the hit point
                Debug.DrawLine(mainCamera.transform.position, hitPoint, Color.red);

                // Get the direction from the current position to the hit point
                Vector3 direction = hitPoint - transform.position;

                // Calculate the rotation to look at the hit point
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }


            // Clamp the camera's rotation
            Vector3 clampedRotation = transform.rotation.eulerAngles;
            clampedRotation.x = Mathf.Clamp(clampedRotation.x, clampRotationX.x, clampRotationX.y);
            transform.rotation = Quaternion.Euler(clampedRotation);
        }

        public void OnEnterGuild()
        {
            cam_anim.SetBool("Reset", false);
            cam_anim.Play("Enter_Guild");
        }

        public void OnEnterDungeon()
        {
            cam_anim.Play("Enter_Dungeon");
        }

        public void ResetTownCam()
        {
            cam_anim.SetBool("Reset", true);
        }

        public void OnReturnTown()
        {
            cam_anim.Play("Return_Town");
        }

        public void ShowDungeonMap()
        {
            menuTownScript.ShowDungeonMap();
        }

        public void ShowTownCanvas()
        {
            menuTownScript.toggle_AllLayout(true);
        }

    }
    
}
