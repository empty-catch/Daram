using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float currentSpeed;

    private Vector2 moveDirection;

    private Action<Monster> monsterGenerateAction;
    private Action<Monster> monsterResetAction;

    private Action<int> monsterDeathAction;
    private Action<float> monsterAttackAction;

    private Action monsterDamageAction;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int defaultHp;
    private int monsterHp;
    private int[] monsterHpKeys;

    [SerializeField]
    private float damage;

    [SerializeField]
    private int score;

    private Image[][] keyImages;

    private Aura aura;
    private Tween auraTween;
    private Tween speedTween;

    private void Awake(){
        currentSpeed = speed;
        keyImages = new Image[8][];

        monsterHp = defaultHp;
        monsterHpKeys = new int[monsterHp];
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        GameObject childObject = gameObject.transform.GetChild(0).gameObject;

        for(int i = 0; i < 8; i++){
            Image[] tempArray = new Image[3];

            for(int j =  i * 3; j < i * 3 + 3; j++){
                tempArray[j - i * 3] = childObject.transform.GetChild(j).GetComponent<Image>();
            }

            keyImages[i] = tempArray;
        }
    }

    public void SettingActions(Action<Monster> monsterGenerateAction, Action<Monster> monsterResetAction, Action<float> monsterAttackAction, Action<int> monsterDeathAction, Action monsterDamageAction){
        this.monsterGenerateAction = monsterGenerateAction;
        this.monsterResetAction = monsterResetAction;
        this.monsterAttackAction = monsterAttackAction;
        this.monsterDeathAction = monsterDeathAction;
        this.monsterDamageAction = monsterDamageAction;
    }

    public void SetAuraFor(Aura aura, float time){
        this.aura = aura;
        auraTween?.Kill();
        auraTween = DOVirtual.DelayedCall(time, () => aura = Aura.None);
    }

    public void SetSpeedFor(float percentage, float time){
        currentSpeed = speed * percentage;
        speedTween?.Kill();
        speedTween = DOVirtual.DelayedCall(time, () => currentSpeed = speed);
    }

    public void GetDamage(){
        GetDamage(monsterHpKeys[0], true);
    }

    public void GetDamage(int key, bool byAbility = false){

        if(monsterHpKeys[0].Equals(key)){
            monsterHp--;

            if (!byAbility){
                monsterDamageAction?.Invoke();
            }

            if(monsterHp <= 0){
                Death();
                return;
            }

            for (int j = 0; j < keyImages[monsterHpKeys[0]].Length; j++){
                if (keyImages[monsterHpKeys[0]][j].enabled.Equals(true)){
                    keyImages[monsterHpKeys[0]][j].enabled = false;
                    break;
                }
            }
            int temp = monsterHpKeys[0];
            monsterHpKeys[0] = monsterHpKeys[1];
            monsterHpKeys[1] = monsterHpKeys[2];
            monsterHpKeys[2] = temp;
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
            keyImages[monsterHpKeys[i]][getAvailableKeyImage(monsterHpKeys[i])].gameObject.transform.SetAsLastSibling();
            keyImages[monsterHpKeys[i]][getAvailableKeyImage(monsterHpKeys[i])].gameObject.SetActive(true);
        }

        moveDirection = (Vector2.zero - (Vector2)gameObject.transform.position).normalized;
        spriteRenderer.flipX = moveDirection.x > 0 ? true : false;

        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        while(true){
            gameObject.transform.Translate(moveDirection * currentSpeed);
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

        for(int i = 0; i < keyImages.Length; i++){
            for(int j = 0; j < keyImages[i].Length; j++){
                keyImages[i][j].enabled = true;
                keyImages[i][j].gameObject.SetActive(false);
                // Debug.Log(keyImages[i][j].enabled);
            }
        }

        gameObject.SetActive(false);
        monsterResetAction(this);
        monsterHp = defaultHp;
    }

}
