using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour, InteractableInterface
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with CannonBall");
        // toggle cannonball in player hand to be visible
        return true;
    }
}
