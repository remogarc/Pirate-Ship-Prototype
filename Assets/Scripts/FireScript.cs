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
    public float damageInterval = 2f;
    private float damageAmount = 1f;
    private bool isTakingDamage = false;
    public ShipHealth shipHealth;

    // Start is called before the first frame update
    void Start()
    {
        fire = transform.parent.GetComponent<ParticleSystem>();
        audioSource = transform.parent.GetComponent<AudioSource>();
    }

    public void enableFire(){
        audioSource.Play();
        fireEnabled = true;
        fire.Play();

        if (!isTakingDamage)
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    public void disableFire(){
        fireEnabled = false;
        fire.Stop();
        fire.Clear();

        if (isTakingDamage)
        {
            StopCoroutine(ApplyDamageOverTime());
            isTakingDamage = false;
        }

        if(shipHealth != null)
        {
            shipHealth.ChangeHealth(-5f); // Restore 5 health when fire is put out
        }
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

    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;
        while (fireEnabled)
        {
            if (shipHealth != null)
            {
                shipHealth.ChangeHealth(damageAmount); // Apply damage to the ship
            }
            yield return new WaitForSeconds(damageInterval); // Wait before applying damage again
        }
        isTakingDamage = false;
    }
}
