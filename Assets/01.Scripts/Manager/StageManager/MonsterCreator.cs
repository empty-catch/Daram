using System.CodeDom.Compiler;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject monstersParentObject;

    private List<Monster> monsterList = new List<Monster>();

    private Vector2 leftGeneratePosition;
    private Vector2 rightGeneratePosition;

    public List<Monster> MonsterList => monsterList;

    private void Awake(){
        leftGeneratePosition = Vector2.zero;
        rightGeneratePosition = Vector2.zero;

        leftGeneratePosition.x = -10;
        rightGeneratePosition.x = 10;

        monsterList = monstersParentObject.GetComponentsInChildren<Monster>(true).ToList();
    }

    [ContextMenu("Monster Generate")]
    public void Execute(){
        Monster generatedMonster;

        generatedMonster = GetAvailableMonster();
        
        // generatedMonster.gameObject.transform.position = Random.Range(0,2).Equals(0)
        // ? leftGeneratePosition
        // : rightGeneratePosition;

        generatedMonster?.Execute();
    }

    private Monster GetAvailableMonster(){
        for(int i = 0; i < monsterList.Count; i++){
            if(monsterList[i].gameObject.activeInHierarchy.Equals(false)){
                return monsterList[i];
            }
        }
        return null;
    }
}
