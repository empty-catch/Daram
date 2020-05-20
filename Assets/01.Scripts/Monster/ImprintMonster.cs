using System;
using UnityEngine;
using DG.Tweening;

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
    private float duration;
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

    public override void GetDamage(int key){
        base.GetDamage(key);
        seal?.Invoke((int)sealGesture);
        DOVirtual.DelayedCall(duration, () => seal?.Invoke(-1));
    }
}
