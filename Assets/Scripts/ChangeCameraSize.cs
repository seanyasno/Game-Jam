using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ChangeCameraSize : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Camera targetCamera = null;

    [Header("Global Data")]
    [SerializeField] private bool changeSize = false;
    [SerializeField] private float newCameraSize = 0;
    [SerializeField] private float changingSpeed = 0;
    [SerializeField] private Vector3 newVector = new Vector3(0, 0, 0);

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag.Equals("Player"))
            StartCoroutine("changeTheSize", 1);
    }

    public IEnumerator changeTheSize() {
        while (Mathf.Abs(targetCamera.orthographicSize - newCameraSize) > 0.1f){
            targetCamera.orthographicSize = Mathf.Lerp(targetCamera.orthographicSize, newCameraSize, changingSpeed * Time.deltaTime);
            targetCamera.GetComponent<CameraFollowTarget>().addedVector = Vector3.Lerp(targetCamera.GetComponent<CameraFollowTarget>().addedVector, newVector, changingSpeed * Time.deltaTime);
            yield return null;
        }
        StopCoroutine("changeTheSize");
    }

}
