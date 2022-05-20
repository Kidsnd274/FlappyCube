using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore() {
        score++;
    }

    public int getScore() {
        return score;
    }

    public void ResetScore() {
        score = 0;
    }
}
