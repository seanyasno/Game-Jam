using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{

    [SerializeField] private Transform followedTarget = null;
    [SerializeField] private Vector3 addedVector = new Vector3(0, 0, 0);
   
    void Start() {

        if (followedTarget == null)
            Debug.Log("Camera Follow Target [14] -> Followed Target Not Found");

    }

   
    void LateUpdate() {

        transform.position = new Vector3(followedTarget.position.x + addedVector.x, followedTarget.position.y + addedVector.y, followedTarget.position.z + addedVector.z);
        
    }
}
