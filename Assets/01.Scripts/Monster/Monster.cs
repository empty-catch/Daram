using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Monster : MonoBehaviour
{
    private Vector2 moveDirection;

    private Action<Monster> monsterGenerateAction;
    private Action<Monster> monsterResetAction;

    private Action<int> monsterDeathAction;
    private Action<float> monsterAttackAction;

    private SpriteRenderer spriteRenderer;

    [Header("Values")]
    [SerializeField]
    private int defaultHp;
    private int monsterHp;
    
    private int[] monsterHpKeys;

    [SerializeField]
    private float defaultSpeed;
    private float speed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private int score;

    private Image[][] keyImages;


    private void Awake(){
        keyImages = new Image[8][];

        monsterHp = defaultHp;
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
    public void SettingActions(params dynamic[] action){
        this.monsterGenerateAction = action[0];
        this.monsterResetAction = action[1];
        this.monsterAttackAction = action[2];
        this.monsterDeathAction = action[3];
    }

    public void GetDamage(int key){
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

        for(int i = 0; i < keyImages.Length; i++){
            for(int j = 0; j < keyImages[i].Length; j++){
                keyImages[i][j].enabled = true;
                keyImages[i][j].gameObject.SetActive(false);
            }
        }

        gameObject.SetActive(false);
        monsterResetAction(this);
        monsterHp = defaultHp;
        speed = defaultSpeed;
    }

}