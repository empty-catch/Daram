using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageController : MonoBehaviour
{
   
   private List<Monster> activeMonsterList = new List<Monster>();

   public void AddActiveMonster(Monster monster){
       activeMonsterList.Add(monster);
   }

   public void RemoveActiveMonster(Monster monster){
       activeMonsterList.Remove(monster);
   }

   public void AttackMonsters(int key){
       for(int i = 0; i < activeMonsterList.Count; i++){
           activeMonsterList[i].GetDamage(key);
       }
   }

   public void MonsterAllDeath(){
        for(int i = 0; i < activeMonsterList.Count; i++){
           activeMonsterList[i].Death();
       }
   }
}
