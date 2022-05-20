using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text startCountdownText;
    public TMP_Text gameOverScore;
    public GameObject gameOverObject;

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

    public void EndGame(int score) { // Hide score and show final score with retry button
        scoreText.gameObject.SetActive(false);
        gameOverScore.text = "Score: " + score.ToString();
        gameOverObject.SetActive(true);
    }

    public void RestartGame() {
        gameOverObject.SetActive(false);
        startCountdownText.gameObject.SetActive(true);
        gameManager.RestartGame();
    }

    public void BackToMainMenu() {
        gameManager.BackToMainMenu();
    }
}
