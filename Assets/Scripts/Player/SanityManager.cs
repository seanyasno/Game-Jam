using System.Collections;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SanityManager : MonoBehaviour
{

    private float decreaseMultipler = 0.25f;
    public float DecreaseMultipler { get { return decreaseMultipler; } set{}}

    // The current amount of sanity the player's has
    [SerializeField, Range(0f, 75f)] private float sanity;
    public float Sanity { get; }

    [SerializeField] private SanityLevel sanityLevel;
    public SanityLevel SanityLevel { get; }

    private CameraShake cameraShake;

    [SerializeField] private Vector3 respawnLocation = new Vector3(0, 0, 0);
    [SerializeField] private Camera cam;

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
        sanity -= toSub/(1/decreaseMultipler);

        if (sanity <= 0) death();
    }

    private void death(){
        sanity = maxLightRange;
        transform.position = respawnLocation;
        cam.orthographicSize = 5;
        cam.GetComponent<CameraFollowTarget>().addedVector = new Vector3(0, 2, -10);
    }

    // Updates player light's settings
    private void UpdateLight() {
        //playerLight.range = sanity;
        playerLight.color = new Color(0.2376291f, (sanity+25f)/144f, 0.3483699f, 1f);

        if (sanity < minLightRange) { // low
            LowPPPUpdate();
            sanityLevel = SanityLevel.LOW;

            //StartCoroutine(cameraShake.Shake(0.1f, 4f));
            CameraShaker.Instance.ShakeOnce(2.5f, 3f, 0.1f, 1f);
        } else if (sanity > maxLightRange) { // high
            HighPPPUpdate();
            sanityLevel = SanityLevel.HIGH;
        } else { // normal
            NormalPPPUpdate();
            sanityLevel = SanityLevel.NORMAL;
        }
    }

    private void LowPPPUpdate() { 
        PPPUpdateHelper(false, false, lowGrainIntesity, lowGrainSize);
    }

    private void NormalPPPUpdate() {
        PPPUpdateHelper(false, false, normalGrainIntesity, normalGrainSize);
    }

    private void HighPPPUpdate() {
        PPPUpdateHelper(true, true, normalGrainIntesity, normalGrainSize);
    }

    private void PPPUpdateHelper(bool motionBlur, bool chromaticAberration, float grainIntensity, float grainSize) {
        ppb.profile.motionBlur.enabled = motionBlur;
        ppb.profile.chromaticAberration.enabled = chromaticAberration;

        GrainModel.Settings grainSettings = ppb.profile.grain.settings;
        grainSettings.intensity = grainIntensity;
        grainSettings.size = grainSize;
        ppb.profile.grain.settings = grainSettings;
    
    }
}

public enum SanityLevel {
    LOW,
    NORMAL,
    HIGH
}