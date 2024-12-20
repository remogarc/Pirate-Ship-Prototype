using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour, InteractableInterface
{

    
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("CannonBall Interacted");
        PlayerController player = interactor.gameObject.GetComponent<PlayerController>();
        if (player.hasCannonBall) return false;
        player.toggleCannonBall(true);
        return true;
    }
}
