using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EarthAbility : IAbility
{
    private GameObject effect;

    public EarthAbility(GameObject effect)
    {
        this.effect = effect;
    }

    public bool IsCooldown { get; private set; }

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

        var gObj = Object.Instantiate(effect, new Vector3(0F, -0.8F), Quaternion.identity);
        gObj.transform.localScale = new Vector3(info.hitCount * 1.5F, info.hitCount * 1.5F);
        Object.Destroy(gObj, info.duration);

        foreach (var monster in activeMonsters)
        {
            switch (monster.Aura)
            {
                case Aura.None:
                case Aura.Lightning:
                case Aura.Wind:
                case Aura.Flame:
                case Aura.Ice when monster.AuraLevel <= level:
                    EarthSkill(monster, info, level);
                    break;
                case Aura.Earth:
                    monster.SetSpeedFor(0F, 0.5F);
                    DOVirtual.DelayedCall(0.5F, () => EarthSkill(monster, info, level));
                    break;
            }
        }
    }

    private void EarthSkill(Monster monster, AbilityInfo.Info info, int level)
    {
        if (Mathf.Abs(monster.transform.position.x) <= info.hitCount &&
            Mathf.Abs(monster.transform.position.y) <= info.hitCount / 2F)
        {
            monster.SetSpeedFor(0.2F, info.duration);
            monster.SetAuraFor(Aura.Earth, level, info.auraDuration);
        }
    }
}
