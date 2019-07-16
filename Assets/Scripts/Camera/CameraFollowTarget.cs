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
// <<<<<<< HEAD

        transform.position = new Vector3(followedTarget.position.x + addedVector.x, followedTarget.position.y + addedVector.y, followedTarget.position.z + addedVector.z);
// =======
//         //followedTarget.position.y + 5
        // transform.position = new Vector3(followedTarget.position.x, transform.position.y, followedTarget.position.z + addToPositionZ);
// >>>>>>> 1f31eef9c41b7649ff69475aa46ed2b15e7d3b65
        
    }
}
