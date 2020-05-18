using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Ability : MonoBehaviour
{
    [SerializeField]
    private MonsterDamageController monsterDamageController;
    [SerializeField]
    private AbilityInfo[] infos;
    [SerializeField]
    private GameObject[] effects;
    private IAbility[] abilities = new IAbility[5];

    private int mana = 100;
    private bool canExecute = true;

    public void Execute(int index)
    {
        index -= 6;
        int level = GetLevel(index);
        var info = infos[index][level];

        if (canExecute && mana >= info.manaCost)
        {
            mana -= info.manaCost;
            canExecute = false;
            DOVirtual.DelayedCall(info.cooldown, () => canExecute = true);
            abilities[index].Execute(monsterDamageController.ActiveMonsters, info, effects[index]);
        }
    }

    public void EarnMana(int amount)
    {
        mana += amount;
    }

    public void EarnMana(int amount, int repeat)
    {
        IEnumerator Coroutine()
        {
            for (int i = 0; i < repeat; i++)
            {
                yield return YieldInstructionCache.WaitingSecond(1F);
                mana += amount;
            }
        }

        StartCoroutine(Coroutine());
    }

    private void Awake()
    {
        abilities[0] = new LightningAbility();
        abilities[1] = new WindAbility();
        abilities[2] = new FlameAbility(repeat => EarnMana(3, repeat));
        abilities[3] = new IceAbility();
        abilities[4] = new LightningAbility();
    }

    private int GetLevel(int index)
    {
        var prefsStr = GetPrefsStr(index);
        return PlayerPrefs.GetInt(prefsStr, 0);
    }

    private string GetPrefsStr(int index)
    {
        switch (index)
        {
            case 0:
                return "ElectricPoint";
            case 1:
                return "WindPoint";
            case 2:
                return "FirePoint";
            case 3:
                return "IcePoint";
            case 4:
                return "EarthPoint";
        }
        throw new ArgumentException();
    }
}
