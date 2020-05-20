using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffMonster : Monster
{
    public enum debuffType { drawReverse, directionReverse}

    private debuffType type;
    private int index;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] spriteResources;

    [Header("Debuff")]
    [SerializeField]
    private IntEvent toggleDebuff;

    protected override void Awake(){
        base.Awake();
        index = Convert.ToInt32(Regex.Replace(transform.name, @"\D", ""));
    }

    public override void Execute(){
        base.Execute();

        int randomValue = UnityEngine.Random.Range(0, 2);

        switch(randomValue){
            case 0:
            type = debuffType.drawReverse;
            spriteRenderer.sprite = spriteResources[0];
            break;

            case 1:
            type = debuffType.directionReverse;
            spriteRenderer.sprite = spriteResources[1];
            break;
        }

        toggleDebuff?.Invoke(index);
    }

    public override void Death(){
        base.Death();
        toggleDebuff?.Invoke(index);
    }
}
