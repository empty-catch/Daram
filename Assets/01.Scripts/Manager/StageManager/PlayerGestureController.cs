using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerGestureController : MonoBehaviour
{
    private Action<int> gestureAction;
    private int[] inputKeys = {0,1,2,3,4,5,6,7,8};

    public void SettingGestureAction(Action<int> gestureAction){
        this.gestureAction = gestureAction;
    }

    private void Update(){
        if(Input.anyKeyDown){
            for(int i = 0; i < inputKeys.Length; i++){
                if(Input.GetKeyDown(inputKeys[i].ToString())){
                    gestureAction(inputKeys[i]);
                }
            }
        }
    }
}
