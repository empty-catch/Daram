using System.Collections.Generic;

public class LightningAbility : IAbility
{
    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            monster.GetDamage(info.removalCount);
            monster.SetSpeedFor(0F, info.duration);
            monster.SetAuraFor(Aura.Lightning, info.auraDuration);
        }
    }
}
