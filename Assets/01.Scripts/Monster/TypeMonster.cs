using System;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMonster : Monster
{
    [Header("Values")]
    [SerializeField]
    private Aura counterType;
    
    [Header("Resources")]
    [SerializeField]
    private Sprite[] spriteResources;

    private void SetCounterType(){
        var type = Enum.GetValues(typeof(Aura));
        counterType = (Aura) type.GetValue(new System.Random().Next(type.Length));  

        switch(counterType){
            case Aura.Earth:
            spriteRenderer.sprite = spriteResources[0];
            break;

            case Aura.Flame:
            spriteRenderer.sprite = spriteResources[1];
            break;
            
            case Aura.Ice:
            spriteRenderer.sprite = spriteResources[2];
            break;
            
            case Aura.Lightning:
            spriteRenderer.sprite = spriteResources[3];
            break;
            
            case Aura.Wind:
            spriteRenderer.sprite = spriteResources[4];
            break;

        }
    }

    public override void Execute(){
        base.Execute();
        SetCounterType();
    }

    public override void SetAuraFor(Aura aura, int level, float time){
        base.SetAuraFor(aura, level, time);
        if(counterType.Equals(aura)){
            Death();
        }
    }

    public override void SetHigherAuraFor(Aura aura, int level, float time){
        base.SetHigherAuraFor(aura, level, time);
    }
}
