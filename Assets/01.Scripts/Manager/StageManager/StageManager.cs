using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private Ability ability;

    private MonsterCreator monsterCreator;
    private MonsterDamageController monsterDamageController;
    private PlayerGestureController playerGestureController;
    private ScoreManager scoreManager;


    [SerializeField]
    private float monsterGenenrateInterval;

    private void Awake()
    {
        monsterCreator = gameObject.GetComponent<MonsterCreator>();
        monsterDamageController = gameObject.GetComponent<MonsterDamageController>();
        playerGestureController = gameObject.GetComponent<PlayerGestureController>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    private void Start()
    {
        playerGestureController.SettingGestureAction(monsterDamageController.AttackMonsters);

        scoreManager.SettingAbilitySkil(monsterDamageController.MonsterAllDeath);

        monsterCreator.MonsterList.ForEach((monster) =>
        {
            monster.SettingActions(
                monsterDamageController.AddActiveMonster,
                monsterDamageController.RemoveActiveMonster,
                scoreManager.GetDamage,
                scoreManager.ScoreUp,
                () => ability.EarnMana(7)
            );
        });

        StartCoroutine(MonsterGenerateCoroutine());
    }

    private IEnumerator MonsterGenerateCoroutine()
    {
        while (true)
        {
            monsterCreator.Execute();
            yield return YieldInstructionCache.WaitingSecond(monsterGenenrateInterval);
        }
    }
}
