using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SanityManager : MonoBehaviour
{

    // The current amount of sanity the player's has
    [SerializeField, Range(0f, 6f)] private float sanity = 5.2f;
    public float Sanity { get; }

    [SerializeField] private SanityLevel sanityLevel;
    public SanityLevel SanityLevel { get; }

    //------------------- Lighting Settings Section ----------------------

    public Light playerLight;

    // settings for player light's spread range
    [SerializeField] private float minLightRange = 3f;
    [SerializeField] private float normalLightRange = 4f;
    [SerializeField] private float maxLightRange = 6f;

    [SerializeField] private Color lowSanityColor = Color.red;
    [SerializeField] private Color normalSanityColor = Color.green;
    [SerializeField] private Color highSanityColor = Color.blue;

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
        playerLight.range = sanity;
        if (sanity < 3.8) { // low
            playerLight.color = lowSanityColor;
            LowPPPUpdate();
            sanityLevel = SanityLevel.LOW;
        } else if (sanity > 4.8) { // high
            playerLight.color = highSanityColor;
            HighPPPUpdate();
            sanityLevel = SanityLevel.HIGH;
        } else { // normal
            playerLight.color = normalSanityColor;
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