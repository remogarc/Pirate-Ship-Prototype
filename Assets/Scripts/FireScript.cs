using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour, InteractableInterface
{
    public bool fireEnabled = false;

    private ParticleSystem fire;

    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        fire = transform.parent.GetComponent<ParticleSystem>();
        audioSource = transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enableFire(){
        audioSource.Play();
        fireEnabled = true;
        fire.Play();
    }

    public void disableFire(){
        fireEnabled = false;
        fire.Stop();
        fire.Clear();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Fire Interacted");
        if (fireEnabled){
            disableFire();
            return false;
        }
        else{
            return true;
        }
        
    }
}
