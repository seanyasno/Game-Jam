using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{

    [SerializeField]
    private Transform followedTarget = null;
    [SerializeField]
    private float addToPositionZ = 0;
   
    void Start() {

        if (followedTarget == null)
            Debug.Log("Camera Follow Target [14] -> Followed Target Not Found");

    }

   
    void LateUpdate() {

        transform.position = new Vector3(followedTarget.position.x, followedTarget.position.y + 5, followedTarget.position.z + addToPositionZ);
        
    }
}
