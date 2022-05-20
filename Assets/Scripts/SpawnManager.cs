using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnRate = 2f; // How many times a second
    public float freeZoneSize = 3f;
    //public float freeZoneHeightPercentage = 0.5f;
    public float maxHeightPercentage = 0.7f;
    public float minHeightPercentage = 0.3f;

    public GameObject obstacle;

    private bool active = false;
    private float randomPercentage;
    private float timer;

    // Start is called before the first frame update
    void Start() {
        timer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            // Update timer
            timer -= Time.deltaTime;
            if (timer <= 0) {
                InstantiateObstacle();
                timer = spawnRate; // Reset timer
            }
        }
    }

    void InstantiateObstacle() {
        float randomValue = UnityEngine.Random.Range(minHeightPercentage, maxHeightPercentage);
        randomPercentage = (float) Math.Round(randomValue * 100f) / 100f;
        GameObject newObstacle = Instantiate(obstacle); // Spawns a new obstacle
        newObstacle.transform.position = transform.position;
        ObstacleController obstacleController = newObstacle.GetComponent<ObstacleController>();
        obstacleController.setObstacleSize(freeZoneSize, randomPercentage);
    }

    public void SetActive(bool active) {
        if (active) {
            this.active = true;
            InstantiateObstacle(); // Spawn one
        } else {
            this.active = false;
            timer = spawnRate; // Reset Timer
        }
    }
}
