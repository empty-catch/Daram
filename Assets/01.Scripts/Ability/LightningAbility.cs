using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightningAbility : IAbility
{
    private GameObject effect;
    private GameObject windEffect;
    private System.Action<Monster, AbilityInfo.Info> iceSkill;

    public bool IsCooldown { get; private set; }

    public LightningAbility(GameObject effect, GameObject windEffect, System.Action<Monster, AbilityInfo.Info> iceSkill)
    {
        this.effect = effect;
        this.windEffect = windEffect;
        this.iceSkill = iceSkill;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, int level)
    {
        if (IsCooldown)
        {
            return;
        }
        else
        {
            IsCooldown = true;
            DOVirtual.DelayedCall(info.cooldown, () => IsCooldown = false);
        }

        for (int i = 0; i < info.hitCount && i < activeMonsters.Count; i++)
        {
            var monster = activeMonsters[i];
            switch (monster.Aura)
            {
                case Aura.None:
                    LightningSkill(monster, info);
                    monster.SetAuraFor(Aura.Lightning, level, info.auraDuration);
                    break;
                case Aura.Lightning:
                    LightningSkill(monster, info);
                    monster.SetAuraFor(Aura.Lightning, level, info.auraDuration);
                    DOVirtual.DelayedCall(info.duration + 0.1F, () =>
                    {
                        monster.GetDamageInevitably(1);
                        var gObj2 = Object.Instantiate(effect, monster.transform.position + new Vector3(0.5F, 0F), Quaternion.identity);
                        Object.Destroy(gObj2, 0.5F);
                    });
                    break;
                case Aura.Wind:
                    LightningSkill(monster, info);
                    DOVirtual.DelayedCall(info.duration + 0.1F, () =>
                    {
                        monster.SetAuraFor(Aura.Lightning, level, info.auraDuration);
                        monster.SetSpeedFor(0F, 0.5F);
                        var windEffectObj = Object.Instantiate(windEffect, monster.transform.position + new Vector3(0.5F, 0F), Quaternion.identity);
                        Object.Destroy(windEffectObj, 0.5F);
                    });
                    break;
                case Aura.Flame:
                    LightningSkill(monster, info);
                    monster.SetHigherAuraFor(Aura.Lightning, level, info.auraDuration);
                    break;
                case Aura.Ice:
                    LightningSkill(monster, info);
                    monster.SetHigherAuraFor(Aura.Lightning, level, info.auraDuration);
                    if (level - 2 >= monster.AuraLevel)
                    {
                        monster.GetDamageInevitably(1);
                    }
                    else
                    {
                        iceSkill?.Invoke(monster, info);
                    }
                    break;
                case Aura.Earth:
                    if (level - 2 >= monster.AuraLevel)
                    {
                        monster.SetAuraFor(Aura.Lightning, level, info.auraDuration);
                    }
                    else
                    {
                        monster.SetAuraFor(Aura.None, 0, 0F);
                    }
                    break;
            }
        }
    }

    private void LightningSkill(Monster monster, AbilityInfo.Info info)
    {
        monster.GetDamageInevitably(info.removalCount);
        monster.SetSpeedFor(0F, info.duration);
        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
        Object.Destroy(gObj, info.duration - 0.1F);
    }
}
