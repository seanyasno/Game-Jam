using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{

    [SerializeField]
    private Transform followedTarget = null;
   
    void Start() {

        if (followedTarget == null)
            Debug.Log("Camera Follow Target [14] -> Followed Target Not Found");

    }

   
    void LateUpdate() {

        transform.position = new Vector3(followedTarget.position.x, followedTarget.position.y, followedTarget.position.z - 10);
        
    }
}
