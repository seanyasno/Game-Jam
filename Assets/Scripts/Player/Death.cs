using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField] private Camera cam;

    [Header("Global Data")]
    [SerializeField] private Vector3 respawnLocation = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start() {
        
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag.Equals("Player")){
            col.transform.position = respawnLocation;
            cam.orthographicSize = 5;
            cam.GetComponent<CameraFollowTarget>().addedVector = new Vector3(0, 2, -10);
        }
        
    }
}
