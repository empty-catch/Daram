using System.Collections.Generic;
using UnityEngine;

public class IceAbility : IAbility
{
    private GameObject effect;

    public IceAbility(GameObject effect)
    {
        this.effect = effect;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, int level)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            monster.SetAuraFor(Aura.Ice, level, info.auraDuration);
            IceSkill(monster, info);
        }
    }

    public void IceSkill(Monster monster, AbilityInfo.Info info)
    {
        monster.SetSpeedFor(0F, info.duration);
        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
        Object.Destroy(gObj, info.duration - 0.1F);
    }
}
