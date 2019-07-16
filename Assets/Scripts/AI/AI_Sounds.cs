using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Sounds : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField] private Transform target = null;
    [SerializeField] private AudioSource audioSource = null;

    [Header("Global Data")]
    [SerializeField] private float radius = 0;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(transform.position, target.position) <= radius && !audioSource.isPlaying)
            audioSource.Play();
        if (Vector3.Distance(transform.position, target.position) > radius && audioSource.isPlaying)
            audioSource.Stop();
    }
}
