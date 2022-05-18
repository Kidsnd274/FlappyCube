using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour // NOT WORKING
{
    public float freeZoneSize = 3f;
    public float freeZoneHeightPercentage = 0.5f;

    private GameObject topObstacle;
    private GameObject freeZone;
    private GameObject bottomObstacle;

    // Start is called before the first frame update
    void Start()
    {
        topObstacle = GetComponent<GameObject>().transform.GetChild(0).gameObject;
        freeZone = GetComponent<GameObject>().transform.GetChild(1).gameObject;
        bottomObstacle = GetComponent<GameObject>().transform.GetChild(2).gameObject;

        setObstacleSize(freeZoneSize, freeZoneHeightPercentage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setObstacleSize(float freeZoneSize, float heightPercentage) {
        float viewportHeight = System.Math.Abs(Camera.main.ViewportToWorldPoint(Vector3.zero).y) * 2;
        float obstacleHeight = (viewportHeight - freeZoneSize) * heightPercentage; // HAVEN'T INTEGRATED HEIGHT PERCENTAGE YET

        // Top 0% Btm 100%
        // Setting freeZone
        freeZone.transform.localScale = new Vector3(freeZone.transform.localScale.x, freeZoneSize, 1);
        freeZone.transform.position = new Vector3(freeZone.transform.position.x, 0);

        // Setting top obstacle
        topObstacle.transform.localScale = new Vector3(freeZone.transform.localScale.x, obstacleHeight, 1);
        float topObstaclePosY = (viewportHeight / 2) - (obstacleHeight / 2);
        topObstacle.transform.position = new Vector3(topObstacle.transform.position.x , topObstaclePosY);

        // Setting bottom obstacle
        bottomObstacle.transform.localScale = new Vector3(freeZone.transform.localScale.x, 1 - obstacleHeight, 1);
        float bottomObstaclePosY = -((viewportHeight / 2) - (1 - obstacleHeight / 2));
        bottomObstacle.transform.position = new Vector3(bottomObstacle.transform.position.x, bottomObstaclePosY);
    }
}
