using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    // Start is called before the first frame update
    public int range = 8;
    // When closest to the player, how fast should the light be "transfered"
    //  to the player.
    public float lightReductionFactor = 0.9f;
    public float startIntensity = 20f, shutdownIntensity = 0.02f;
    Light light;
    private GameObject player;
 
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(!player)
        {
            Debug.Log("Player not found!");
            Destroy(this.gameObject);
        }
    }
 
    void Start()
    {
        light = GetComponent<Light>();
        light.intensity = startIntensity;
    }

    private void Update()
    {
        float distanceFromPlayer = DistanceFromPlayer();
        if(distanceFromPlayer <= range)
        {
            float normalizedDistance = distanceFromPlayer / range;
            float reductionFactor = lightReductionFactor*(1-normalizedDistance) + normalizedDistance;

            // We need to detemine two things: how much light to reduce,
            //  and how much light should we pass to player.
            float previousIntesity = light.intensity;
            light.intensity = light.intensity * reductionFactor;
            if(light.intensity <= shutdownIntensity)
                light.intensity = 0;
       
            SanityManager playerSanity = player.GetComponent<SanityManager>();
            if(!playerSanity)
            {
                Debug.Log("ERROR: no SanityManager script attached to the player.");
                Destroy(this.gameObject);
            }
            playerSanity.IncreaseSanity(previousIntesity - light.intensity);
        }
    }
    private float DistanceFromPlayer()
    {
        return Vector3.Distance(this.transform.position, player.transform.position);
    }
}
