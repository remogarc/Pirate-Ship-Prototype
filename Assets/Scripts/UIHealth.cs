using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealth : MonoBehaviour
{
    public TMP_Text healthDisplay;
    public ShipHealth shipHealth;

    void Start()
    {
        
    }

    void Update()
    {
        healthDisplay.text = "Health: " + shipHealth.currentHealth.ToString("0");
    }
}
