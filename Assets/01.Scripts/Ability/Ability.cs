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
    [SerializeField]
    private GameObject[] windEffects;
    [SerializeField]
    private GameObject burnEffect;
    [SerializeField]
    private GameObject fog;
    private IAbility[] abilities = new IAbility[5];
    private bool canExecute = true;

    public int Mana { get; set; } = 100;

    public void Execute(int index)
    {
        index -= 6;
        int level = GetLevel(index);
        var info = infos[index][level];

        if (canExecute && Mana >= info.manaCost)
        {
            Mana -= info.manaCost;
            // canExecute = false;
            // DOVirtual.DelayedCall(info.cooldown, () => canExecute = true);
            abilities[index].Execute(monsterDamageController.ActiveMonsters, info, level);
        }
    }

    private void Awake()
    {
        Action<float> burn = duration => DOTween.To(() => Mana, i => Mana = i, Mana + (int)duration * 3, duration).SetEase(Ease.Linear);
        abilities[3] = new IceAbility(effects[3]);
        abilities[0] = new LightningAbility(effects[0], windEffects[0], (abilities[3] as IceAbility).IceSkill);
        abilities[1] = new WindAbility(effects[1], windEffects[0], windEffects[1], burnEffect, windEffects[2], windEffects[3], burn);
        abilities[2] = new FlameAbility(effects[2], burnEffect, windEffects[1], fog, burn, (abilities[3] as IceAbility).IceSkill);
        abilities[4] = new EarthAbility(effects[4]);
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
