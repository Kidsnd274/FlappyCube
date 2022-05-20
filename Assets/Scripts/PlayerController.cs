using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Camera mainCamera;
    private GameManager gameManager;
    private bool outOfBoundsVelocitySet = false;
    private int counter = 0;

    public float maxVelocity = 10f;
    public float jumpForce = 20f;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null) {

        }

        if (keyboard.spaceKey.wasPressedThisFrame && gameManager.IsGameStart()) {
            Jump();
        }
    }

    private void FixedUpdate() {
        ClampObjectIntoView();

        // Set max velocity
        if (playerRb.velocity.y > maxVelocity) {
            playerRb.velocity = new Vector2(playerRb.velocity.x, maxVelocity);
        } else if (playerRb.velocity.y < -maxVelocity) {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -maxVelocity);
        }
    }

    private void Jump() {
        //Debug.Log("Jumping with force " + jumpForce.ToString() + " and counter " + counter.ToString());
        counter++;
        playerRb.velocity = new Vector2(0, 0);
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void ClampObjectIntoView() {
        float frustumPositionBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0)).y;
        float frustumPositionTop = Math.Abs(mainCamera.ViewportToWorldPoint(new Vector3(0, 0)).y);

        // Set position and velocity
        if (playerRb.position.y < frustumPositionBottom) {
            //Debug.Log("Below window! Sending to " + frustumPositionBottom.ToString());
            playerRb.position = new Vector2(playerRb.position.x, frustumPositionBottom);
            if (!outOfBoundsVelocitySet) {
                playerRb.velocity = Vector2.zero;
                outOfBoundsVelocitySet = true;
            }
        } else if (playerRb.position.y > frustumPositionTop) {
            //Debug.Log("Above window! Sending to " + frustumPositionTop.ToString());
            playerRb.position = new Vector2(playerRb.position.x, frustumPositionTop);
            if (!outOfBoundsVelocitySet) {
                playerRb.velocity = Vector2.zero;
                outOfBoundsVelocitySet = true;
            }
        } else {
            outOfBoundsVelocitySet = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Player collided with something!");
        if (other.gameObject.tag == "Obstacle") {
            gameManager.AddToScore();
        }
    }

    /**
     * Unfreezes the player
     */
    public void StartGame() {
        playerRb.constraints = RigidbodyConstraints2D.None;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRb.AddForce(Vector2.zero, ForceMode2D.Impulse); // Force update rb
    }
}
