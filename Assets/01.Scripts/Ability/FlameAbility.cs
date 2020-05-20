using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlameAbility : IAbility
{
    private GameObject effect;
    private GameObject burnEffect;
    private GameObject windEffect;
    private GameObject fog;

    private System.Action<float> burn;
    private System.Action<Monster, AbilityInfo.Info> iceSkill;

    public bool IsCooldown { get; private set; }

    public FlameAbility(GameObject effect, GameObject burnEffect, GameObject windEffect, GameObject fog, System.Action<float> burn, System.Action<Monster, AbilityInfo.Info> iceSkill)
    {
        this.effect = effect;
        this.burnEffect = burnEffect;
        this.windEffect = windEffect;
        this.fog = fog;
        this.burn = burn;
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
                    monster.SetAuraFor(Aura.Flame, level, info.auraDuration);
                    FlameSkill(monster, info, effect);
                    break;
                case Aura.Lightning:
                    monster.SetHigherAuraFor(Aura.Flame, level, info.auraDuration);
                    FlameSkill(monster, info, effect);
                    break;
                case Aura.Wind:
                    FlameSkill(monster, info, windEffect);
                    monster.SetAuraFor(Aura.Flame, level, info.auraDuration);
                    DOVirtual.DelayedCall(info.duration, () =>
                    {
                        burn?.Invoke(info.duration);
                        monster.GetDamageInevitably(1);
                        var burnObj = Object.Instantiate(burnEffect, monster.transform);
                        Object.Destroy(burnObj, info.duration);
                    });
                    break;
                case Aura.Ice:
                    monster.SetHigherAuraFor(Aura.Flame, level, info.auraDuration);
                    var gObj = Object.Instantiate(fog, monster.transform);
                    Object.Destroy(gObj, info.duration);
                    if (monster.AuraLevel > level)
                    {
                        iceSkill?.Invoke(monster, info);
                    }
                    else
                    {
                        FlameSkill(monster, info, effect);
                    }
                    break;
                case Aura.Earth:
                    FlameSkill(monster, info, effect);
                    if (monster.AuraLevel - 2 >= level)
                    {
                        monster.SetHigherAuraFor(Aura.None, -1, 0F);
                    }
                    else
                    {
                        monster.SetAuraFor(Aura.Flame, level, info.auraDuration);
                    }
                    break;
            }
        }
    }

    private void FlameSkill(Monster monster, AbilityInfo.Info info, GameObject effect)
    {
        burn?.Invoke(info.duration);
        monster.GetDamageInevitably(info.removalCount);

        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
        Object.Destroy(gObj, 0.5F);
        var burnObj = Object.Instantiate(burnEffect, monster.transform);
        Object.Destroy(burnObj, info.duration);
    }
}
