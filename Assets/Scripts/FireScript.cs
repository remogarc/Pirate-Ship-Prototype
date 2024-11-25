using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public bool fireEnabled = false;

    private ParticleSystem fire;


    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (fireEnabled)
            {
                disableFire();
            }
            else
            {
                enableFire();
            }
        }
    }

    public void enableFire(){
        fireEnabled = true;
        fire.Play();
    }

    public void disableFire(){
        fireEnabled = false;
        fire.Stop();
        fire.Clear();
    }



}
