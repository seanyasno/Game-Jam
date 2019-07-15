using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    
    public void switchScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}
