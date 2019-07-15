using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalMobsManager : MonoBehaviour
{

    public Transform[] spawnLocations;

    private SanityManager sanityManager;

    private void Awake() {
        sanityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SanityManager>();
        if (sanityManager == null)
            Debug.LogError("PLAYER IS MISSING");
    }

    private void Update() {
        
    }

}

[CreateAssetMenu(fileName ="mob name", menuName ="Mob")]
public class Mob : ScriptableObject {

    public GameObject mobPrefab;
    public int minSpawnAmount;
    public int maxSpawnAmount;

}

