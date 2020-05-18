using System.Collections.Generic;

public class LightningAbility : IAbility
{
    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info)
    {
        for (int i = 0, removal = 0; i < info.hitCount; i++, removal++)
        {
            var monster = activeMonsters[i];
            monster.SetSpeedFor(0F, info.duration);
            monster.SetAuraFor(Aura.Lightning, info.auraDuration);

            if (removal < info.removalCount)
            {
                monster.GetDamage();
            }
        }
    }
}
