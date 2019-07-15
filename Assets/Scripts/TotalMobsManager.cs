using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalMobsManager : MonoBehaviour
{

    public Mob[] mobsToSpawn;

    public Transform[] spawnLocations;

    [SerializeField] private List<MobSpawnManager> mobSpawnManager;

    private SanityManager sanityManager;

    private void Awake() {
        sanityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SanityManager>();
        if (sanityManager == null)
            Debug.LogError("PLAYER IS MISSING");

        mobSpawnManager = new List<MobSpawnManager>();
    }

    private void Update() {
        switch (sanityManager.SanityLevel) {
            case SanityLevel.LOW:
                break;
            case SanityLevel.NORMAL:
                break;
            case SanityLevel.HIGH:
                break;
        }
    }

}

[CreateAssetMenu(fileName ="mob name", menuName ="Mob")]
public class Mob : ScriptableObject {

    public GameObject mobPrefab;

}

