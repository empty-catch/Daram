﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartSceneManager : MonoBehaviour
{  
    public void AbilityScene(){
        SceneManager.LoadScene("AbilityScene");
    }

    public void StartGame(){
        SceneManager.LoadScene("MainScene");
    }    
}
