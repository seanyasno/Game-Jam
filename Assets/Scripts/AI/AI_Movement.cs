using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{

    [SerializeField]
    private Transform followedTarget = null;
    [SerializeField]
    public int currentMarkerIndex = 1;

    [SerializeField]
    private float movementSpeed = 0;


    void Start() {

        if (followedTarget == null)
            Debug.Log("AI Movement [16] -> Followed Target Not Found");

        if (transform.childCount <= 1)
            Debug.Log("AI Movement [21] -> Path Markers Are Empty");
            
    }

    // Update is called once per frame
    void Update() {

        if (Vector3.Distance(this.transform.GetChild(0).position, transform.GetChild(currentMarkerIndex).position) <= .2f) {
            currentMarkerIndex++;
            if (currentMarkerIndex == transform.childCount)
                currentMarkerIndex = 1;
        }

        transform.GetChild(0).position = Vector3.MoveTowards(this.transform.GetChild(0).position, transform.GetChild(currentMarkerIndex).position, movementSpeed * Time.deltaTime);
    }
}
