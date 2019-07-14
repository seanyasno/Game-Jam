using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{

    [SerializeField]
    private Transform followedTarget = null;
    [SerializeField]
    private bool needToFollowTarget = false;
    [SerializeField]
    private float distanceToGetCaught = 0f;
    [SerializeField]
    private float distanceToEscape = 0f;

    [SerializeField]
    public int currentMarkerIndex = 1;

    [SerializeField]
    private float movementSpeed = 0;


    void Start() {

        if (followedTarget == null)
            Debug.Log("AI Movement [16] -> Followed Target Not Found");

        if (transform.childCount <= 1)
            Debug.Log("AI Movement [21] -> Path Markers Are Empty");

        needToFollowTarget = false;
            
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Vector3.Distance(transform.GetChild(0).position, followedTarget.position) <= distanceToGetCaught && !needToFollowTarget)
            needToFollowTarget = true;

        if (Vector3.Distance(transform.GetChild(0).position, followedTarget.position) > distanceToEscape && needToFollowTarget)
            needToFollowTarget = false;

        switch (needToFollowTarget)
        {
            case true:
                transform.GetChild(0).position = Vector3.MoveTowards(this.transform.GetChild(0).position, followedTarget.position, movementSpeed * Time.deltaTime);
                break;

            case false:
                if (Vector3.Distance(this.transform.GetChild(0).position, transform.GetChild(currentMarkerIndex).position) <= .2f)
                {
                    currentMarkerIndex++;
                    if (currentMarkerIndex == transform.childCount)
                        currentMarkerIndex = 1;
                }

                transform.GetChild(0).position = Vector3.MoveTowards(this.transform.GetChild(0).position, transform.GetChild(currentMarkerIndex).position, movementSpeed * Time.deltaTime);
                break;
        }

    }
}
