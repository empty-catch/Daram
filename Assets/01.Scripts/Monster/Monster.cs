﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int defaultHp;
    private int monsterHp;
    private int[] monsterHpKeys;

    [SerializeField]
    private float damage;

    [SerializeField]
    private int score;

    [SerializeField]
    private GameObject[] keyObjects;
    
    private Image[][] keyImages;


    private void Awake(){
        keyImages = new Image[keyObjects.Length][];

        monsterHp = defaultHp;
        monsterHpKeys = new int[monsterHp];
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        for(int i = 0; i < keyObjects.Length; i++){
            keyImages[i] = keyObjects[i].GetComponentsInChildren<Image>(true); 
        }
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
                keyImages[monsterHpKeys[i]][0].gameObject.SetActive(false);
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
            monsterHpKeys[i] = UnityEngine.Random.Range(0,8);
        }

        Func<int, int> getAvailableKeyImage = (value) => {
            for(int i = 0; i < keyImages.Length; i++){
                if(keyImages[value][i].gameObject.activeInHierarchy.Equals(false)){
                    return i;
                }
            }

            return -1;
        };

        for(int i = 0; i < monsterHpKeys.Length; i++){
            keyImages[monsterHpKeys[i]][getAvailableKeyImage(monsterHpKeys[i])].gameObject.SetActive(true);
        }

        moveDirection = (Vector2.zero - (Vector2)gameObject.transform.position).normalized;
        spriteRenderer.flipX = moveDirection.x > 0 ? true : false;

        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        while(true){
            gameObject.transform.Translate(moveDirection * speed);
            yield return YieldInstructionCache.WaitFrame;

            if(gameObject.transform.position.x * moveDirection.x > -2){
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

        for(int i = 0; i < keyImages.Length; i++){
            for(int j = 0; j < keyImages.Length; j++){
                keyImages[i][j].gameObject.SetActive(false);
            }
        }

        monsterResetAction(this);
        monsterHp = defaultHp;
    }
}
