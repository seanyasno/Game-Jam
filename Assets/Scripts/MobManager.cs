using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{

    public float damangeReductionFactor = 0.9f;
    [Range(0f, 50f)] public float range;

    private SanityManager sanityManager;
    private GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        sanityManager = player.GetComponent<SanityManager>();
    }

    private void Update() {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceFromPlayer <= range) {

            float normalizedDistance = distanceFromPlayer / range;
            float reductionFactor = damangeReductionFactor * (1 - normalizedDistance) + normalizedDistance;

            print(reductionFactor);

            sanityManager.DecreaseSanity(reductionFactor * 0.1f);
        }
    }


}
