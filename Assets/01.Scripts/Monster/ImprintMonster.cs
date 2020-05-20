using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprintMonster : Monster
{
    private enum gesture {vertical, up, horizontal, down, downArrow, upArrow}
    
    [Header("Values")]
    [SerializeField]
    private gesture sealGesture;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] spriteResources;

    private void SetRandomSealGesture(){
        var gestures = Enum.GetValues(typeof(gesture));
        sealGesture = (gesture) gestures.GetValue(new System.Random().Next(gestures.Length)); 

        switch(sealGesture){
            case gesture.vertical:
            spriteRenderer.sprite = spriteResources[0];
            break;

            case gesture.up:
            spriteRenderer.sprite = spriteResources[1];
            break;

            case gesture.horizontal:
            spriteRenderer.sprite = spriteResources[2];
            break;
            
            case gesture.down:
            spriteRenderer.sprite = spriteResources[3];
            break;
            
            case gesture.downArrow:
            spriteRenderer.sprite = spriteResources[4];
            break;

            case gesture.upArrow:
            spriteRenderer.sprite = spriteResources[5];
            break;
        }
    }

    public override void Execute(){
        base.Execute();
        SetRandomSealGesture();
    }

    public override void GetDamage(int key){
        base.GetDamage(key);

        // TODO : 제스쳐 봉인 만들기
        
    }

    
}
