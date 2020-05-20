using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMonster : Monster
{
    [SerializeField]
    private Aura counterType;
    
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
