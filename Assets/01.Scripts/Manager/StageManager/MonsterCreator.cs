using System.CodeDom.Compiler;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private GameObject monstersParentObject;

    private List<Monster> monsterList = new List<Monster>();
    private List<int> beforeGeneratePositionIndex = new List<int>();

    [SerializeField]
    private Transform[] monsterGeneratePositions;
    public List<Monster> MonsterList => monsterList;

    private void Awake(){
        monsterList = monstersParentObject.GetComponentsInChildren<Monster>(true).ToList();
    }

    [ContextMenu("Monster Generate")]
    public void Execute(){
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        int randomRepeatIndex = Random.Range(0, 5);
        
        for(int i = 0; i < randomRepeatIndex; i++){
            Monster generatedMonster;
            int monsterGeneratePositionIndex = Random.Range(0, monsterGeneratePositions.Length);
                
            if(beforeGeneratePositionIndex.Contains(monsterGeneratePositionIndex))  
                continue;

            generatedMonster = GetAvailableMonster();
            generatedMonster.gameObject.transform.position = monsterGeneratePositions[monsterGeneratePositionIndex].position;
            generatedMonster?.Execute();

            beforeGeneratePositionIndex.Add(monsterGeneratePositionIndex);

            yield return YieldInstructionCache.WaitingSecond(0.1f);
        }

        beforeGeneratePositionIndex.Clear();
    }

    private Monster GetAvailableMonster(){
        int i = 0;
        int randomIndex;

        do{
            randomIndex = Random.Range(0, monsterList.Count);
            if(monsterList[randomIndex].gameObject.activeInHierarchy.Equals(false)){
                return monsterList[randomIndex];
            }
            i++;
        }while(i < 5);

        return null;
    }
}
