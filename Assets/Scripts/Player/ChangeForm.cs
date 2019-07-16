using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeForm : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private SanityManager managerOfSanity;

    [Header("Global Data")]
    [SerializeField] public bool hasTransformed = false;
    [SerializeField] private Sprite normalForm = null;
    [SerializeField] public List<Transform> transformOptions = new List<Transform>();
    [SerializeField] private Transform items = null;
    [SerializeField] private float radius = 0f;
    [SerializeField] private float increaseOfSanity = 0f;

    [Header("Mana Control")]
    [SerializeField] public float maxMana = 0f;
    [SerializeField] public float currentMana = 0f;
    [SerializeField] private float speedOfManaLoss = 0f;
    [SerializeField] private Image manaBar = null;

    private void Start(){
        hasTransformed = false;
        GetComponent<SpriteRenderer>().sprite = normalForm;
        managerOfSanity = GetComponent<SanityManager>();

        currentMana = maxMana;
        manaBar.fillAmount = maxMana;

    }

    private void Update(){

        if (transformOptions.Count == 0)
            hasTransformed = false;

        getOptions();
        transformed();

        if (Input.GetKeyDown(KeyCode.X)){

            switch(hasTransformed){

                case true:
                    transformPlayer(transform, true);
                    break;

                case false:
                    if (transformOptions.Count > 0 && currentMana > 0)
                        transformPlayer(transformOptions[Random.Range(0, transformOptions.Count)], false);
                    break;

            }
            hasTransformed = !hasTransformed;
        }

    }

    private void getOptions(){

        transformOptions.Clear();
        foreach (Transform item in items){
            if (Vector3.Distance(item.position, this.transform.position) <= radius)
                transformOptions.Add(item.transform);
        }

    }

    private void transformPlayer(Transform positionOfItem, bool enableScripts){

        cam.position = new Vector3(positionOfItem.position.x, positionOfItem.position.y, positionOfItem.position.z - 10);

        cam.GetComponent<CameraShaker>().enabled = enableScripts;
        cam.GetComponent<CameraFollowTarget>().enabled = enableScripts;
        GetComponent<PlayerMovement>().enabled = enableScripts;
        GetComponent<Animator>().enabled = enableScripts;
        GetComponent<SpriteRenderer>().enabled = enableScripts;
        
        foreach (Transform obj in transform){
            if (obj.GetComponent<Light>() != null)
                obj.GetComponent<Light>().enabled = enableScripts;
        }

    }

    private void transformed(){

        if (hasTransformed){
             managerOfSanity.IncreaseSanity(Time.deltaTime * increaseOfSanity);
             currentMana -= Time.deltaTime * speedOfManaLoss;
             updateMana(currentMana);

             if (currentMana <= 0){
                updateMana(0);
                transformPlayer(transform, true);
                hasTransformed = false;
             }
        }

    }

    public void updateMana(float newValue){
          currentMana = newValue;
          manaBar.fillAmount = newValue;
    }

}
