using System.Collections.Generic;
using UnityEngine;

public class FlameAbility : IAbility
{
    private GameObject effect;
    private GameObject burnEffect;
    private System.Action<float> burn;

    public FlameAbility(GameObject effect, GameObject burnEffect, System.Action<float> burn)
    {
        this.effect = effect;
        this.burnEffect = burnEffect;
        this.burn = burn;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, int level)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            monster.SetAuraFor(Aura.Flame, level, info.auraDuration);
            FlameSkill(monster, info);
        }
    }

    private void FlameSkill(Monster monster, AbilityInfo.Info info)
    {
        burn?.Invoke(info.duration);
        monster.GetDamage(info.removalCount);

        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
        Object.Destroy(gObj, 0.5F);
        var burnObj = Object.Instantiate(burnEffect, monster.transform);
        Object.Destroy(burnObj, info.duration);
    }
}
