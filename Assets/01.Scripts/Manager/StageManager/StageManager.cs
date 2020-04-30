using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private MonsterCreator monsterCreator;

    [SerializeField]
    private float monsterGenenrateInterval;

    private void Awake(){
        monsterCreator = gameObject.GetComponent<MonsterCreator>();
    }

    private void Start(){
        StartCoroutine(MonsterGenerateCoroutine());
    }

    private IEnumerator MonsterGenerateCoroutine(){
        while(true){
            monsterCreator.Execute();
            yield return YieldInstructionCache.WaitingSecond(monsterGenenrateInterval);
        }
    }

}
