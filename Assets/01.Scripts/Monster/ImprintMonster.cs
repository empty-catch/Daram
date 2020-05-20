using System;
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

    [Header("Seal")]
    [SerializeField]
    private IntEvent seal;

    private void SetRandomSealGesture(){
        var gestures = Enum.GetValues(typeof(gesture));
        sealGesture = (gesture) gestures.GetValue(new System.Random().Next(gestures.Length));
        spriteRenderer.sprite = spriteResources[(int)sealGesture];
    }

    public override void Execute(){
        base.Execute();
        SetRandomSealGesture();
    }

    public override void GetDamage(int key, bool isByAbility = false){
        base.GetDamage(key);
        seal?.Invoke((int)sealGesture);
    }
}
