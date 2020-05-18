using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, GameObject effect);
}
