using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private static readonly float defaultTimerStart = 3.5f;

    public ScoreManager scoreManager;
    public SpawnManager spawnManager;
    public UIManager uiManager;
    public PlayerController player;

    private bool gameStart = false;
    private bool timerStart = false;
    private float timer = defaultTimerStart;

    // Start is called before the first frame update
    void Start()
    {
        StartGameTimer();
    }
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
        #if UNITY_EDITOR
        QualitySettings.vSyncCount = 1;  // VSync must be disabled = 0
        Application.targetFrameRate = -1;
        #endif
    }

    private void Update() {
        if (timerStart) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                GameStart();
                timerStart = false;
                timer = defaultTimerStart;
                UpdateTimerUI();
            } else {
                UpdateTimerUI();
            }
        }
    }

    public void GameStart() {
        player.StartGame(); // Unfreeze player
        gameStart = true;
        uiManager.HideStartingTimerAndStartScore(); // Hides timer and shows score
        UpdateScoreUI(); // Updates the score to 0
        spawnManager.SetActive(true); // Start spawning obstacles
    }

    public void GameEnd() { // Obstacle should report Player death (or shld player report its own death)
        gameStart = false;
        spawnManager.SetActive(false);
        uiManager.EndGame(scoreManager.getScore());
    }

    public void RestartGame() {
        GameObject[] allObstacles = GameObject.FindGameObjectsWithTag("ObstacleMain");
        foreach (GameObject obstacle in allObstacles) {
            Destroy(obstacle);
        }
        scoreManager.ResetScore();
        Destroy(player.gameObject); // Recreate Player
        player = spawnManager.InstantiateNewPlayer().GetComponent<PlayerController>();
        StartGameTimer();
    }

    // Timer methods
    private void StartGameTimer() { // Starts countdown timer
        timerStart = true;
    }

    private void UpdateTimerUI() {
        int currentTime = Mathf.FloorToInt(timer);
        uiManager.UpdateStartCountdownTimer(currentTime);
    }

    // Score methods
    public void AddToScore() {
        if (gameStart) {
            scoreManager.AddToScore();
            int currentScore = scoreManager.getScore();
            uiManager.UpdateScore(currentScore);
        }
    }

    public void UpdateScoreUI() {
        int currentScore = scoreManager.getScore();
        uiManager.UpdateScore(currentScore);
    }

    public bool IsGameStart() {
        return gameStart;
    }
}
