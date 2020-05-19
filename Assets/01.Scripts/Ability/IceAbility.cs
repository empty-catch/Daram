using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceAbility : IAbility
{
    private GameObject effect;
    private GameObject windEffect;

    public IceAbility(GameObject effect, GameObject windEffect)
    {
        this.effect = effect;
        this.windEffect = windEffect;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, int level)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            switch (monster.Aura)
            {
                case Aura.None:
                case Aura.Earth:
                    IceSkill(monster, info);
                    monster.SetAuraFor(Aura.Ice, level, info.auraDuration);
                    break;
                case Aura.Lightning:
                    IceSkill(monster, info);
                    monster.SetAuraFor(Aura.Ice, level, info.auraDuration);
                    DOVirtual.DelayedCall(info.duration, () => monster.SetSpeedFor(0F, info.duration / 2F));
                    break;
                case Aura.Wind:
                    monster.SetAuraFor(Aura.Ice, level, info.auraDuration);
                    if (monster.AuraLevel > level)
                    {
                        var gObj = Object.Instantiate(windEffect, monster.transform.position, Quaternion.identity);
                        Object.Destroy(gObj, info.duration);
                    }
                    else
                    {
                        IceSkill(monster, info);
                    }
                    break;
                case Aura.Flame:
                    monster.SetHigherAuraFor(Aura.Ice, level, info.auraDuration);
                    if (monster.AuraLevel <= level)
                    {
                        IceSkill(monster, info);
                    }
                    break;
                case Aura.Ice:
                    IceSkill(monster, info);
                    monster.SetAuraFor(Aura.Ice, level, info.auraDuration);
                    DOVirtual.DelayedCall(info.duration, () =>
                    {
                        monster.SetSpeedFor(0F, info.duration * 1.5F);
                        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
                        Object.Destroy(gObj, info.duration - 0.1F);
                    });
                    break;
            }
        }
    }

    public void IceSkill(Monster monster, AbilityInfo.Info info)
    {
        monster.SetSpeedFor(0F, info.duration);
        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
        Object.Destroy(gObj, info.duration - 0.1F);
    }
}
