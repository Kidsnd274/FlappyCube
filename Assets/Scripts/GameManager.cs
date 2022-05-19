using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore() {
        scoreManager.AddToScore();
    }
}
