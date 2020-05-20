using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffMonster : Monster
{
    public enum debuffType { drawReverse, directionReverse}

    private debuffType type;
    private SpriteRenderer spriteRenderer;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] spriteResources;

    private void Awake(){
        base.BaseAwake();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Execute(){
        base.Execute();
        
        int randomValue = Random.Range(0, 2);

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

        StartCoroutine(AbilityCoroutine());
    }

    private IEnumerator AbilityCoroutine(){
        while(true){
            // TOOD : 디버프 만들기
            yield return YieldInstructionCache.WaitingSecond(0.25f);
        }
    }  
}
