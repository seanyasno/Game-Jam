using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SanityManager : MonoBehaviour
{

    // The current amount of sanity the player's has
    [SerializeField, Range(0f, 75f)] private float sanity;
    public float Sanity { get; }

    [SerializeField] private SanityLevel sanityLevel;
    public SanityLevel SanityLevel { get; }

    //------------------- Lighting Settings Section ----------------------

    public Light playerLight;

    // settings for player light's spread range
    [SerializeField] private float minLightRange = 30f;
    [SerializeField] private float normalLightRange = 45f;
    [SerializeField] private float maxLightRange = 60f;

    //---------------- Post Processing Settings Section -------------------

    public PostProcessingBehaviour ppb;

    private float normalGrainIntesity = 0.216f;
    private float normalGrainSize = 0.79f;
    private float lowGrainIntesity = 0.54f;
    private float lowGrainSize = 1.87f;


    private void Awake() {
        if (ppb == null) {
            Debug.LogError("PPB IS MISSING");
        }
    }

    private void Update() {
        DecreaseSanity(Time.deltaTime / 2);
        UpdateLight();
    }
   
    public void IncreaseSanity(float toAdd) {
        sanity += toAdd;

        if (sanity >= maxLightRange) sanity = maxLightRange;
    }

    public void DecreaseSanity(float toSub) {
        sanity -= toSub/8;

        if (sanity <= minLightRange) sanity = minLightRange;
    }

    // Updates player light's settings
    private void UpdateLight() {
        //playerLight.range = sanity;
        playerLight.color = new Color(0.2376291f, (sanity+25f)/144f, 0.3483699f, 1f);
        if (sanity < 3.8) { // low
            LowPPPUpdate();
            sanityLevel = SanityLevel.LOW;
        } else if (sanity > 4.8) { // high
            HighPPPUpdate();
            sanityLevel = SanityLevel.HIGH;
        } else { // normal
            NormalPPPUpdate();
            sanityLevel = SanityLevel.NORMAL;
        }
    }

    private void LowPPPUpdate() { 
        ppb.profile.motionBlur.enabled = false;
        ppb.profile.chromaticAberration.enabled = false;

        GrainModel.Settings grainSettings = ppb.profile.grain.settings;
        grainSettings.intensity = lowGrainIntesity;
        grainSettings.size = lowGrainSize;
        ppb.profile.grain.settings = grainSettings;
    }

    private void NormalPPPUpdate() {
        ppb.profile.motionBlur.enabled = false;
        ppb.profile.chromaticAberration.enabled = false;

        GrainModel.Settings grainSettings = ppb.profile.grain.settings;
        grainSettings.intensity = normalGrainIntesity;
        grainSettings.size = normalGrainSize;
        ppb.profile.grain.settings = grainSettings;
    }

    private void HighPPPUpdate() {
        ppb.profile.motionBlur.enabled = true;
        ppb.profile.chromaticAberration.enabled = true;

        GrainModel.Settings grainSettings = ppb.profile.grain.settings;
        grainSettings.intensity = normalGrainIntesity;
        grainSettings.size = normalGrainSize;
        ppb.profile.grain.settings = grainSettings;
    }
}

public enum SanityLevel {
    LOW,
    NORMAL,
    HIGH
}