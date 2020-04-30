using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector2 moveDirection;

    private Action<Monster> monsterGenerateAction;
    private Action<Monster> monsterResetAction;


    private int[] monsterHp = new int[3];

    public void SettingActions(Action<Monster> monsterGenerateAction, Action<Monster> monsterResetAction){
        this.monsterGenerateAction = monsterGenerateAction;
        this.monsterResetAction = monsterResetAction;
    }

    public void Execute(){
        gameObject.SetActive(true);
        
        monsterGenerateAction(this);

        for(int i = 0; i < monsterHp.Length; i++){
            monsterHp[i] = UnityEngine.Random.Range(0,9);
        }

        moveDirection = (Vector2.zero - (Vector2)gameObject.transform.position).normalized;
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        while(true){
            gameObject.transform.Translate(moveDirection * speed);
            yield return YieldInstructionCache.WaitFrame;

            if(gameObject.transform.position.x * moveDirection.x > -1){
                ResetObject();
            }
        }       
    }

    public void ResetObject(){
        gameObject.SetActive(false);
        monsterResetAction(this);
    }
}
