using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInteract : MonoBehaviour, InteractableInterface
{
    public int locationIndex;
    private AudioSource audioSource;
    private EventManager eventManager;
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Cannon Interacted");
        PlayerController player = interactor.gameObject.GetComponent<PlayerController>();
        if (!player.hasCannonBall) return false;
        player.toggleCannonBall(false);
        FireCannon();
        return true;
    }

    public void FireCannon(){
        eventManager.destroyShip(locationIndex);
        audioSource.Play();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        if (eventManager == null){
            Debug.LogError("No Event Manager found");
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
