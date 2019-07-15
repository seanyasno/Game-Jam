using System.Collections;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SanityManager : MonoBehaviour
{

    // The current amount of sanity the player's has
    [SerializeField, Range(0f, 75f)] private float sanity;
    public float Sanity { get; }

    [SerializeField] private SanityLevel sanityLevel;
    public SanityLevel SanityLevel { get; }

    private CameraShake cameraShake;

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

        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Update() {
        DecreaseSanity(Time.deltaTime / 2);
        UpdateLight();
    }
   
    public void IncreaseSanity(float toAdd) {
        sanity += toAdd*10;

        if (sanity >= maxLightRange) sanity = 75f;
    }

    public void DecreaseSanity(float toSub) {
        sanity -= toSub/4;

        if (sanity <= 0) sanity = 0;
    }

    // Updates player light's settings
    private void UpdateLight() {
        //playerLight.range = sanity;
        playerLight.color = new Color(0.2376291f, (sanity+25f)/144f, 0.3483699f, 1f);

        if (sanity < minLightRange) { // low
            LowPPPUpdate();
            sanityLevel = SanityLevel.LOW;

            //StartCoroutine(cameraShake.Shake(0.1f, 4f));
            CameraShaker.Instance.ShakeOnce(5f, 10f, 0.1f, 1f);
        } else if (sanity > maxLightRange) { // high
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