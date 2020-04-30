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
}
