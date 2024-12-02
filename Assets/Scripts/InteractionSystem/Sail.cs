using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour, InteractableInterface
{
    public bool sailEnabled = false;
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;  // Interaction prompt string shown to the player

    public GameObject sail;  // The sail GameObject
    //private AudioSource audioSource;

    // Called at the start to initialize components
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();  // Initialize audioSource if attached to the same GameObject
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Sail Interacted");

        // If the sail is enabled, disable it
        if (sailEnabled)
        {
            disableSail();
        }
        else
        {
            enableSail();
        }
        return true;
    }

    // Open the sail
    public void enableSail()
    {
        Debug.Log("Enabling Sail...");
        //if (audioSource != null)
        //{
        //    audioSource.Play();
        //}
        sail.SetActive(true);
        sailEnabled = true;
    }

    // Close the sail
    public void disableSail()
    {
        Debug.Log("Disabling Sail...");
        //if (audioSource != null)
        //{
        //    audioSource.Stop();
        //}
        sail.SetActive(false);
        sailEnabled = false;
    }
}
