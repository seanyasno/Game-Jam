using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Transform body = null;
    [SerializeField] private Animator animator = null; 
    [SerializeField] private List<Transform> pathMarkers = new List<Transform>();

    [Header("Global Data")]
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float defaultGravity = 0;

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
    [SerializeField] private bool wasTargetCaught = false;

    [Header("Run Away")]
    [SerializeField] private float runningSpeed = 15f;
    [SerializeField] private float enemyDetectingRadius = 7f;


    void Start() {

        followedTarget = GameObject.FindWithTag("Player").transform;
        initPathFollowing();
        initTargetChase();

        animator = body.GetComponent<Animator>();
        body.GetComponent<Rigidbody2D>().gravityScale = defaultGravity;
        followedTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate() {

        followPath();
        chaseTarget();
        RunAway();

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

            //if (followedTarget == null)
            //    Debug.Log("AI_Movement [87] -> Followed Target is Missing");

            wasTargetCaught = false;

        }

    }

    private void followPath(){

        if (canFollowPath){

            if (pathMarkers.Count > 0 && !wasTargetCaught) {

                    if (Vector3.Distance(body.position, pathMarkers[currentMarkerIndex].position) <= markerRadius) {
                        currentMarkerIndex = resetIndexOfList(currentMarkerIndex, pathMarkers.Count);
                    }
                    move(pathMarkers[currentMarkerIndex].position, movementSpeed);
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
                move(followedTarget.position, chasingSpeed);

        }

    }

    private void RunAway(){
        ChangeForm changeForm = followedTarget.GetComponent<ChangeForm>();
        if (changeForm != null && changeForm.hasTransformed && Vector3.Distance(body.position, followedTarget.position) <= enemyDetectingRadius){
            move(body.position - followedTarget.position, runningSpeed);
            //flipX(moveDir);
        }
    }

    private int resetIndexOfList(int index, int lstCount){
        index++;
        if (index == lstCount)
            index = 0;
        return index;
    }

    private void move(Vector3 newLocation, float speed){
        animator.SetFloat("movementSpeed", speed);

        newLocation = new Vector3(newLocation.x, newLocation.y, body.position.z);
        body.position = Vector3.MoveTowards(body.position, newLocation, speed * Time.deltaTime);
        flipX(newLocation);
    }

    private void flipX(Vector3 newLocation){
        if (body.position.x > newLocation.x)
            body.GetComponent<SpriteRenderer>().flipX = false;
        else
            body.GetComponent<SpriteRenderer>().flipX = true;
    }

}
