using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForm : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField] private Sprite firstForm = null;
    [SerializeField] private Sprite secondForm = null;
    
    [Header("Global Data")]
    [SerializeField] public bool hasTransformed = false;

    private void Start(){
        hasTransformed = false;
        GetComponent<SpriteRenderer>().sprite = firstForm;
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.X)){

            switch(hasTransformed){

                case true:
                    changeSprite(firstForm, true);
                    break;

                case false:
                     changeSprite(secondForm, false);
                    break;

            }
            hasTransformed = !hasTransformed;
        }
    }

    private void changeSprite(Sprite form, bool enableScripts){

        GetComponent<SpriteRenderer>().sprite = form;
        GetComponent<PlayerMovement>().enabled = enableScripts;
        GetComponent<Animator>().enabled = enableScripts;

    }
}
