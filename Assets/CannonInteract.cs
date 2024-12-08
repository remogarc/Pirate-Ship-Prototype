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
    private GameObject indicator;
    private GameObject cannon;
    private Renderer cannonRenderer;
    private Material originalCannonMaterial;
    private Renderer indicatorRenderer;
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
        indicator = this.transform.GetChild(1).gameObject;
        cannon = this.transform.GetChild(0).gameObject;
        cannonRenderer = cannon.GetComponent<Renderer>();
        indicatorRenderer = indicator.GetComponent<Renderer>();
        originalCannonMaterial = new Material(cannonRenderer.material);
    }

    // Update is called once per frame
    void Update()
    {
        if (eventManager.enemyShips[locationIndex] != null)
        {
            cannonRenderer.material = indicatorRenderer.material;
        }
        else
        {
            cannonRenderer.material = originalCannonMaterial;
        }
    }
}
