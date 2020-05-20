using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprintMonster : Monster
{
    private enum gesture {vertical, up, horizontal, down, downArrow, upArrow, zigzag}

    [SerializeField]
    private gesture sealGesture;

    private void SetRandomSealGesture(){
        var gestures = Enum.GetValues(typeof(gesture));
        sealGesture = (gesture) gestures.GetValue(new System.Random().Next(gestures.Length)); 
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
