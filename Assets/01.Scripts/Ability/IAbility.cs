using System.Collections.Generic;

public interface IAbility
{
    bool IsCooldown { get; }
    void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, int level);
}
