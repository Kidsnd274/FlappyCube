using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour // NOT WORKING
{
    public float speed = 1f;

    private GameObject topObstacle;
    private GameObject freeZone;
    private GameObject bottomObstacle;

    //What’s the difference between Start and Awake?
    //Start and Awake work in similar ways except that Awake is called first and, unlike Start, will be called even if the script component is disabled.
    //Using Start and Awake together is useful for separating initialisation tasks into two steps.For example, a script’s self-initialisation (e.g.creating component references and initialising variables) can be done in Awake before another script attempts to access and use that data in Start, avoiding errors.

    void Awake() {
        topObstacle = this.gameObject.transform.GetChild(0).gameObject;
        freeZone = this.gameObject.transform.GetChild(1).gameObject;
        bottomObstacle = this.gameObject.transform.GetChild(2).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    public void setObstacleSize(float freeZoneSize, float heightPercentage) {
        //Debug.Log("Setting obstacle size! " + heightPercentage.ToString());
        float halfHeight = System.Math.Abs(Camera.main.ViewportToWorldPoint(Vector3.zero).y);
        float viewportHeight = halfHeight * 2;

        // Top 0% Btm 100%
        // Setting freeZone
        freeZone.transform.localScale = new Vector3(freeZone.transform.localScale.x, freeZoneSize, 1);

        float freeZoneMinBottom = freeZoneSize / 2; // also half-length of freeZone
        float freeZoneMaxTop = viewportHeight - (freeZoneSize / 2);
        float freeZoneVariableDistance = freeZoneMaxTop - freeZoneMinBottom;
        float freeZonePosNonNeg = ((1 - heightPercentage) * freeZoneVariableDistance) + freeZoneMinBottom;
        float freeZonePosY = freeZonePosNonNeg - halfHeight;
        freeZone.transform.position = new Vector3(freeZone.transform.position.x, freeZonePosY);

        // Setting top obstacle
        float topObstacleHeight = viewportHeight - freeZonePosNonNeg - freeZoneMinBottom;
        topObstacle.transform.localScale = new Vector3(freeZone.transform.localScale.x, topObstacleHeight, 1);
        float topObstaclePosY = viewportHeight - (topObstacleHeight / 2) - 5;
        topObstacle.transform.position = new Vector3(topObstacle.transform.position.x , topObstaclePosY);

        // Setting bottom obstacle
        float bottomObstacleHeight = viewportHeight - topObstacleHeight - freeZoneSize;
        bottomObstacle.transform.localScale = new Vector3(freeZone.transform.localScale.x, bottomObstacleHeight, 1);
        float bottomObstaclePosY = (bottomObstacleHeight / 2) - 5;
        bottomObstacle.transform.position = new Vector3(bottomObstacle.transform.position.x, bottomObstaclePosY);
    }
}
