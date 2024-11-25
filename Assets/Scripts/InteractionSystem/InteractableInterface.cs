using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractableInterface 
{
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor);
}
