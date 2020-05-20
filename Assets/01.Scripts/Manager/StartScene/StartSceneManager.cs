using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartSceneManager : MonoBehaviour
{  

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    public void AbilityScene(){
        SceneManager.LoadScene("AbilityScene");
    }

    public void StartGame(){
        SceneManager.LoadScene("MainScene");
    }    
}
