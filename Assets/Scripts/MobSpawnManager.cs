using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    private SanityManager sanityManager;

    [SerializeField] private Mob[] mobsToSpawn;
    [SerializeField] private int minSpawnAmount = 3;
    [SerializeField] private int maxSpawnAmount = 8;

    private int amount = 0;
    private List<GameObject> spawnedMobs;

    private float timeToSpawn = 0f;
    private bool canSpawn = true;

    private void Awake() {
        spawnedMobs = new List<GameObject>();
        sanityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SanityManager>();
        if (sanityManager == null)
            Debug.LogError("PLAYER IS MISSING");
    }

    private void Update() {
        switch (sanityManager.SanityLevel) {
            case SanityLevel.LOW:
                if (amount < minSpawnAmount && canSpawn && timeToSpawn <= 0f) {
                    GameObject go = Instantiate(mobsToSpawn[0].mobPrefab, transform.position, Quaternion.identity, transform);
                    spawnedMobs.Add(go);
                    canSpawn = false;
                    timeToSpawn = 3f;
                }
                break;
            case SanityLevel.NORMAL:
                break;
            case SanityLevel.HIGH:
                break;
        }

        if (!canSpawn) {
            if (timeToSpawn < 0f) {
                return;
            }
            timeToSpawn -= Time.deltaTime;
        }
    }


}
