using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    
    public void switchScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void exitGame(string name) {
        Application.Quit();
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
             SceneManager.LoadScene("MainMenu");
    }

}
