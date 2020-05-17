using System;

public class LightningAbility : IAbility
{
    public void Execute(AbilityInfo.Info info, Action<float> callback)
    {
        callback?.Invoke(info.cooldown);
    }
}
