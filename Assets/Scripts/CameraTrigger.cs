using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera cameraInTrigger;  // Camera to switch to when entering trigger
    public Camera cameraOutTrigger; // Camera to switch back to when exiting trigger

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hello");
            // Switch to the camera in the trigger zone
            cameraInTrigger.gameObject.SetActive(true);
            cameraOutTrigger.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that exited the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Switch back to the camera outside the trigger zone
            cameraInTrigger.gameObject.SetActive(false);
            cameraOutTrigger.gameObject.SetActive(true);
        }
    }
}
