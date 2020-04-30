using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private MonsterCreator monsterCreator;
    private MonsterDamageController monsterDamageController;
    private PlayerGestureController playerGestureController;

    [SerializeField]
    private float monsterGenenrateInterval;

    private void Awake(){
        monsterCreator = gameObject.GetComponent<MonsterCreator>();
        monsterDamageController = gameObject.GetComponent<MonsterDamageController>();
        playerGestureController = gameObject.GetComponent<PlayerGestureController>();
    }

    private void Start(){
        playerGestureController.SettingGestureAction(monsterDamageController.AttackMonsters);

        monsterCreator.MonsterList.ForEach((monster) => {
            monster.SettingActions(monsterDamageController.AddActiveMonster, monsterDamageController.RemoveActiveMonster);        
        });

        StartCoroutine(MonsterGenerateCoroutine());
    }

    private IEnumerator MonsterGenerateCoroutine(){
        while(true){
            monsterCreator.Execute();
            yield return YieldInstructionCache.WaitingSecond(monsterGenenrateInterval);
        }
    }
}
