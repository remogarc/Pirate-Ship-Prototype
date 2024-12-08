using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UITimer : MonoBehaviour
{
    public TMP_Text timerDisplay;
    public float countdownTime = 180f; // 3 minutes

    private void Start()
    {
        UpdateTimerDisplay();
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1);
            countdownTime -= 1;
            UpdateTimerDisplay();
        }
        SceneManager.LoadScene("Win scene");
        Debug.Log("Ship has survived!");
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        timerDisplay.text = $"Time: {minutes:00}:{seconds:00}"; //display in MM:SS format
    }
}

