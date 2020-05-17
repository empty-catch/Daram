using System;

public interface IAbility
{
    void Execute(AbilityInfo.Info info, Action<float> callback);
}
