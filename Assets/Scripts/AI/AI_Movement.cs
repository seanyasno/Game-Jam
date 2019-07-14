using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Transform body = null;
    [SerializeField] private List<Transform> pathMarkers = new List<Transform>();

    [Header("Global Data")]
    [SerializeField] private float movementSpeed = 0;

    [Header("Path Following")]
    [SerializeField] private bool canFollowPath = false;
    [SerializeField] private float markerRadius = 0;
    private int currentMarkerIndex = 0;

    [Header("Target Chase")]
    [SerializeField] private bool canChaseTarget = false;
    [SerializeField] private Transform followedTarget = null;
    [SerializeField] private float chasingSpeed = 0f;
    [SerializeField] private float caughtRadius = 0f;
    [SerializeField] private float escapeRadius = 0f;
    private bool wasTargetCaught = false;


    void Start() {

        initPathFollowing();
        initTargetChase();

    }

    void FixedUpdate() {

        followPath();
        chaseTarget();

    }

    private void initPathFollowing(){

        if (canFollowPath){

            currentMarkerIndex = 0;
            
            if (pathMarkers.Count == 0)
                Debug.Log("AI_Movement [82] -> Path Markers Are Empty");

        }

    }

    private void initTargetChase(){

        if (canChaseTarget){

            if (followedTarget == null)
                Debug.Log("AI_Movement [87] -> Followed Target is Missing");

            wasTargetCaught = false;

        }

    }

    private void followPath(){

        if (canFollowPath){

            if (pathMarkers.Count > 0)
                {
                    if (Vector3.Distance(body.position, pathMarkers[currentMarkerIndex].position) <= markerRadius)
                    {
                        currentMarkerIndex = resetIndexOfList(currentMarkerIndex, pathMarkers.Count);
                    }

                    body.position = Vector3.MoveTowards(body.position, pathMarkers[currentMarkerIndex].position, movementSpeed * Time.deltaTime);
                }

        }

    }

    private void chaseTarget(){

        if(canChaseTarget){

            if (Vector3.Distance(body.position, followedTarget.position) <= caughtRadius && !wasTargetCaught)
                wasTargetCaught = true;

            if (Vector3.Distance(body.position, followedTarget.position) > escapeRadius && wasTargetCaught)
                wasTargetCaught = false;

            if (wasTargetCaught)
                body.position = Vector3.Lerp(body.position, followedTarget.position, chasingSpeed * Time.deltaTime);

        }

    }

    private int resetIndexOfList(int index, int lstCount){
        index++;
        if (index == lstCount)
            index = 0;
        return index;
    }
}
