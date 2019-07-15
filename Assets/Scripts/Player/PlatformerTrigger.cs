using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformerTrigger : MonoBehaviour
{
    public TilemapCollider2D tmCollider;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Tilemap")
            tmCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.name == "Tilemap")
            tmCollider.isTrigger = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            tmCollider.isTrigger = true;
        }
    }


    //private void OnTriggerEnter(Collider other) {
    //    print(other.transform.name);
    //}

    //private void OnCollisionEnter(Collision collision) {
    //    print(collision.transform.name);
    //}
}
