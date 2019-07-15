using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformerTrigger : MonoBehaviour
{
    public TilemapCollider2D tmCollider;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Platforms")
            tmCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.name == "Platforms")
            tmCollider.isTrigger = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            tmCollider.isTrigger = true;
        }
    }

}
