using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformerTrigger : MonoBehaviour
{
   public TilemapCollider2D tmCollider;

    public PlayerMovement playerMovement;

    private bool isJumping = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (isJumping) {
            if (other.transform.name == "Platforms") {
                tmCollider.isTrigger = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {

        if (isJumping) {
            if (other.transform.name == "Platforms") {
                tmCollider.isTrigger = true;
            }
        } else {
            if (other.transform.name == "Platforms") {
                tmCollider.isTrigger = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (isJumping) {
            if (other.transform.name == "Platforms") {
                tmCollider.isTrigger = false;
                isJumping = false;
            }
        }
    }

    private void Update() {
        if (playerMovement.Grounded)
            isJumping = false;

        if (Input.GetAxisRaw("Vertical") < 0) {
            tmCollider.isTrigger = true;
            isJumping = true;
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            isJumping = true;
        }
    }
}
