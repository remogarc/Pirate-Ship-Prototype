using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMessage : MonoBehaviour
{
    public TMP_Text messageDisplay;
    public ShipHealth shipHealth;
    public UITimer currentTime;
    public PlayerController playerController;
    public GameObject eventManager;
    void Start()
    {
        StartCoroutine(ClearMessage());
    }
    IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(3f);
        messageDisplay.text = "";
    }

    void Update()
    {
        if (shipHealth.currentHealth <= 0)
        {
            messageDisplay.text = "Your ship has been destroyed!";
            eventManager.GetComponent<EventManager>().enabled = false;
            playerController.enabled = false;
            Time.timeScale = 0;
        }
        else if (currentTime.countdownTime == 0 && shipHealth.currentHealth > 0)
        {
            messageDisplay.text = "Your ship has survived!";
            eventManager.GetComponent<EventManager>().enabled = false;
            playerController.enabled = false;
            Time.timeScale = 0;
        }
    }
}
