﻿using System.Collections;
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

    private Action<int> monsterDeathAction;
    private Action<float> monsterAttackAction;

    [SerializeField]
    private int defaultHp;
    private int monsterHp;
    private int[] monsterHpKeys;

    [SerializeField]
    private float damage;

    [SerializeField]
    private int score;

    private void Awake(){
        monsterHp = defaultHp;
        monsterHpKeys = new int[monsterHp];
    }

    public void SettingActions(Action<Monster> monsterGenerateAction, Action<Monster> monsterResetAction, Action<float> monsterAttackAction, Action<int> monsterDeathAction){
        this.monsterGenerateAction = monsterGenerateAction;
        this.monsterResetAction = monsterResetAction;
        this.monsterAttackAction = monsterAttackAction;
        this.monsterDeathAction = monsterDeathAction;
    }

    public void GetDamage(int key){
        for(int i = 0; i < monsterHpKeys.Length; i++){
            if(monsterHpKeys[i].Equals(key)){
                monsterHp--;

                if(monsterHp <= 0){
                    Death();
                }

                break;
            }
        }
    }  

    public void Execute(){
        gameObject.SetActive(true);
        
        monsterGenerateAction(this);

        for(int i = 0; i < monsterHpKeys.Length; i++){
            monsterHpKeys[i] = UnityEngine.Random.Range(0,9);
        }

        moveDirection = (Vector2.zero - (Vector2)gameObject.transform.position).normalized;
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        while(true){
            gameObject.transform.Translate(moveDirection * speed);
            yield return YieldInstructionCache.WaitFrame;

            if(gameObject.transform.position.x * moveDirection.x > -1){
                Attack();
            }
        }       
    }

    public void Attack(){
        monsterAttackAction(damage);
        ResetObject();
    }

    public void Death(){
        monsterDeathAction(score);
        ResetObject();
    }

    public void ResetObject(){
        gameObject.SetActive(false);
        monsterResetAction(this);
        monsterHp = defaultHp;
    }
}