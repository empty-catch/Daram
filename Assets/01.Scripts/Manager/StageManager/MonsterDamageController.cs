using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageController : MonoBehaviour
{
   private List<Monster> activeMonsterList = new List<Monster>();
   public List<Monster> ActiveMonsters => activeMonsterList;
   
   public void AddActiveMonster(Monster monster){
      activeMonsterList.Add(monster);
   }

   public void RemoveActiveMonster(Monster monster){
      activeMonsterList.Remove(monster);
   }

   public void AttackMonsters(int key){
      int beforeCount = activeMonsterList.Count;

      for(int i = 0; i < activeMonsterList.Count; i++){
         
         if(beforeCount > activeMonsterList.Count){
            beforeCount = activeMonsterList.Count;
            i--;
         }

         activeMonsterList[i].GetDamage(key);
      }
   }

   public void MonsterAllDeath(){
      int reapetCount = activeMonsterList.Count;
      for(int i = 0; i < reapetCount; i++){
         activeMonsterList[0].Death();
      }
   }
}
