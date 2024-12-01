using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRange = 0.75f;
    [SerializeField] private LayerMask interactionLayer;
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numInteractableObjs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numInteractableObjs = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRange, colliders, interactionLayer);
        if(numInteractableObjs > 0)
        {
            var interactable = colliders[0].GetComponent<InteractableInterface>();
            if(interactable != null && Input.GetButtonDown("Fire1"))
            {
                interactable.Interact(this);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }  
}
