using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text startCountdownText;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score) {
        scoreText.text = score.ToString();
    }

    public void UpdateStartCountdownTimer(int timer) {
        startCountdownText.text = "Starting: " + timer.ToString();
    }

    public void HideStartingTimerAndStartScore() {
        //startCountdownText.enabled = false; // or use
        startCountdownText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
}
