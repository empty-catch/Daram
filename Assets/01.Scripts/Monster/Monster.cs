using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
public class Monster : MonoBehaviour
{
    private Vector2 moveDirection;

    private Action<Monster> monsterGenerateAction;
    private Action<Monster> monsterResetAction;

    private Action monsterDamageAction;
    private Action<int> monsterDeathAction;
    private Action<float> monsterAttackAction;

    [Header("Values")]
    private int monsterHp;
    
    private int[] monsterHpKeys;

    [SerializeField]
    private float defaultSpeed;
    private float currentSpeed;
    private float speed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private int score;

    [Header("Objects")]
    [SerializeField]
    private Image auraImage;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] auraSprites;

    private Image[][] keyImages;

    protected SpriteRenderer spriteRenderer;

    private Tween auraTween;
    private Tween speedTween;

    private float auraTime;

    public Aura Aura { get; private set; }
    public int AuraLevel { get; private set; }


    private void Awake(){
        keyImages = new Image[8][];

        monsterHp = UnityEngine.Random.Range(1,5);
        monsterHpKeys = new int[monsterHp];

        speed = defaultSpeed;

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

    // StageManager의 Start 함수에서 설정 해 줌
    public void SettingActions(Action<Monster> monsterGenerateAction, Action<Monster> monsterResetAction, Action<float> monsterAttackAction, Action<int> monsterDeathAction, Action monsterDamageAction){
        this.monsterGenerateAction = monsterGenerateAction;
        this.monsterResetAction = monsterResetAction;
        this.monsterAttackAction = monsterAttackAction;
        this.monsterDeathAction = monsterDeathAction;
        this.monsterDamageAction = monsterDamageAction;
    }


    private void SetAuraImage(Aura aura){

        auraImage.gameObject.SetActive(true);

        switch(aura){
            case Aura.Earth:
            auraImage.sprite = auraSprites[0];
            break;

            case Aura.Flame:
            auraImage.sprite = auraSprites[1];
            break;

            case Aura.Ice:
            auraImage.sprite = auraSprites[2];
            break;

            case Aura.Lightning:
            auraImage.sprite = auraSprites[3];
            break;

            case Aura.Wind:
            auraImage.sprite = auraSprites[4];
            break;
            
            default:
            auraImage.gameObject.SetActive(false);
            break;
        }
    }

    public virtual void SetAuraFor(Aura aura, int level, float time){
        Aura = aura;
        AuraLevel = level;
        auraTime = time;
        auraTween?.Kill();
        auraTween = DOVirtual.DelayedCall(time, () => Aura = Aura.None);
        SetAuraImage(aura);
    }

    public virtual void SetHigherAuraFor(Aura aura, int level, float time){
        if (AuraLevel > level){
            SetAuraFor(Aura, AuraLevel, auraTime);
        }
        else{
            SetAuraFor(aura, level, time);
        }
    }

    public void SetSpeedFor(float percentage, float time){
        currentSpeed = speed * percentage;
        speedTween?.Kill();
        speedTween = DOVirtual.DelayedCall(time, () => currentSpeed = speed);
    }

    public virtual void GetDamage(int key){
        if(monsterHpKeys[0].Equals(key)){
            monsterHp--;

            if(monsterHp <= 0){
                Death();
                return;
            }
    
            for (int i = 0; i < keyImages[monsterHpKeys[0]].Length; i++){
                if (keyImages[monsterHpKeys[0]][i].enabled.Equals(true)){
                    keyImages[monsterHpKeys[0]][i].enabled = false;
                    break;
                }
            }

            int temp = monsterHpKeys[0];
            for(int i = 0; i < monsterHpKeys.Length; i++){
                monsterHpKeys[i] = i.Equals(monsterHpKeys.Length - 1)
                ? temp : monsterHpKeys[i+1];
            }
        }
    }

    public virtual void Execute(){
        gameObject.SetActive(true);

        monsterGenerateAction(this);

        for(int i = 0; i < monsterHpKeys.Length; i++){
            monsterHpKeys[i] = UnityEngine.Random.Range(0,6);
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
            gameObject.transform.Translate(moveDirection * speed);
            yield return YieldInstructionCache.WaitFrame;

            if(gameObject.transform.position.x * (moveDirection.x) > -2){
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
            }
        }

        gameObject.SetActive(false);
        monsterResetAction(this);
        monsterHp = UnityEngine.Random.Range(1,5);
        monsterHpKeys = new int[monsterHp];
        speed = defaultSpeed;
    }

}