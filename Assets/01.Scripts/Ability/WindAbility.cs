using System.Collections.Generic;
using DG.Tweening;

public class WindAbility : IAbility
{
    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            monster.SetSpeedFor(0F, info.duration);
            monster.SetAuraFor(Aura.Wind, info.auraDuration);

            DOTween.Sequence()
            .Append(monster.transform.DOLocalMoveY(1F, 0.2F).SetEase(Ease.OutCubic).SetRelative())
            .AppendInterval(info.duration - 0.3F)
            .Append(monster.transform.DOLocalMoveY(-1F, 0.1F).SetEase(Ease.InQuad).SetRelative());
        }
    }
}
