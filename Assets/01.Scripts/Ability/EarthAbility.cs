using System.Collections.Generic;
using UnityEngine;

public class EarthAbility : IAbility
{
    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, GameObject effect)
    {
        var gObj = Object.Instantiate(effect, new Vector3(0F, -0.8F), Quaternion.identity);
        gObj.transform.localScale = new Vector3(info.hitCount * 1.5F, info.hitCount * 1.5F);
        Object.Destroy(gObj, info.duration);

        foreach (var monster in activeMonsters)
        {
            if (Mathf.Abs(monster.transform.position.x) <= info.hitCount &&
                Mathf.Abs(monster.transform.position.y) <= info.hitCount / 2F)
            {
                monster.SetSpeedFor(0.25F, info.duration);
                monster.SetAuraFor(Aura.Earth, info.auraDuration);
            }
        }
    }
}
