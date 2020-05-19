using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindAbility : IAbility
{
    private GameObject effect;

    public WindAbility(GameObject effect)
    {
        this.effect = effect;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, int level)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            monster.SetAuraFor(Aura.Wind, level, info.auraDuration);
            WindSkill(monster, info);
        }
    }

    private void WindSkill(Monster monster, AbilityInfo.Info info)
    {
        monster.SetSpeedFor(0F, info.duration);
        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
        Object.Destroy(gObj, info.duration - 0.5F);

        DOTween.Sequence()
        .Append(monster.transform.DOLocalMoveY(2.5F, 0.2F).SetEase(Ease.OutCubic).SetRelative())
        .AppendInterval(info.duration - 0.7F)
        .Append(monster.transform.DOLocalMoveY(-2.5F, 0.5F).SetEase(Ease.InQuad).SetRelative());
    }
}
