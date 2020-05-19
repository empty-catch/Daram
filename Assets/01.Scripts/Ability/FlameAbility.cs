using System.Collections.Generic;
using UnityEngine;

public class FlameAbility : IAbility
{
    private System.Action<float> burn;

    public FlameAbility(System.Action<float> burn)
    {
        this.burn = burn;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, GameObject effect)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            burn?.Invoke(info.duration);
            monster.GetDamage(info.removalCount);

            var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
            Object.Destroy(gObj, 0.5F);
        }
    }
}
