using System.Collections.Generic;

public interface IAbility
{
    void Execute(List<Monster> activeMonsters, AbilityInfo.Info info);
}
