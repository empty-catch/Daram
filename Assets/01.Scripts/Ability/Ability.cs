using System;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField]
    private AbilityInfo[] infos;
    private IAbility[] abilities;
    private int mana;
    private bool canExecute;

    public void Execute(int index)
    {
        index -= 6;
        int level = GetLevel(index);

        if (canExecute)
        {
            abilities[index].Execute(infos[index][level]);
        }
    }

    private void Awake()
    {
        abilities[0] = new LightningAbility();
        abilities[1] = new LightningAbility();
        abilities[2] = new LightningAbility();
        abilities[3] = new LightningAbility();
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
