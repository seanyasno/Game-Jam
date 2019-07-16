using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformerTrigger : MonoBehaviour
{
    public TilemapCollider2D tmCollider;

    bool pressedDown() {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Platforms")
            tmCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.name == "Platforms")
            tmCollider.isTrigger = false;
    }

    private void Update() {
// <<<<<<< HEAD
//         if (Input.GetAxisRaw("Vertical") < 0) {
// =======
        if (pressedDown()) {
// >>>>>>> 6f7805f4380ac6c99e7bfa93308b51810d995ca6
            tmCollider.isTrigger = true;
        }
    }

}
