using System.Collections.Generic;
using UnityEngine;

public class FlameAbility : IAbility
{
    private GameObject burnEffect;
    private System.Action<float> burn;

    public FlameAbility(GameObject burnEffect, System.Action<float> burn)
    {
        this.burnEffect = burnEffect;
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
            var burnObj = Object.Instantiate(burnEffect, monster.transform);
            Object.Destroy(burnObj, info.duration);
        }
    }
}
