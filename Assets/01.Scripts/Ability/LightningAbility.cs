using System.Collections.Generic;
using UnityEngine;

public class LightningAbility : IAbility
{
    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, GameObject effect)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            monster.GetDamage(info.removalCount);
            monster.SetSpeedFor(0F, info.duration);
            monster.SetAuraFor(Aura.Lightning, info.auraDuration);

            var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
            Object.Destroy(gObj, info.duration - 0.1F);
        }
    }
}
