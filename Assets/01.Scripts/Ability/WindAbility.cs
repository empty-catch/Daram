using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindAbility : IAbility
{
    private GameObject effect;
    private GameObject lightningEffect;
    private GameObject flameEffect;
    private GameObject burnEffect;
    private GameObject iceEffect;
    private GameObject earthEffect;

    private System.Action<float> burn;

    public bool IsCooldown { get; private set; }

    public WindAbility(GameObject effect, GameObject lightningEffect, GameObject flameEffect, GameObject burnEffect, GameObject iceEffect, GameObject earthEffect, System.Action<float> burn)
    {
        this.effect = effect;
        this.lightningEffect = lightningEffect;
        this.flameEffect = flameEffect;
        this.burnEffect = burnEffect;
        this.iceEffect = iceEffect;
        this.earthEffect = earthEffect;
        this.burn = burn;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info, int level)
    {
        if (IsCooldown)
        {
            return;
        }
        else
        {
            IsCooldown = true;
            DOVirtual.DelayedCall(info.cooldown, () => IsCooldown = false);
        }

        for (int i = 0; i < info.hitCount && i < activeMonsters.Count; i++)
        {
            var monster = activeMonsters[i];
            switch (monster.Aura)
            {
                case Aura.None:
                    WindSkill(monster, info, effect);
                    monster.SetAuraFor(Aura.Wind, level, info.auraDuration);
                    break;
                case Aura.Lightning:
                    WindSkill(monster, info, effect);
                    monster.SetAuraFor(Aura.Wind, level, info.auraDuration);
                    DOVirtual.DelayedCall(info.duration + 0.1F, () =>
                    {
                        monster.SetAuraFor(Aura.Lightning, level, info.auraDuration);
                        monster.SetSpeedFor(0F, 0.5F);
                        var windEffectObj = Object.Instantiate(lightningEffect, monster.transform.position + new Vector3(0.5F, 0F), Quaternion.identity);
                        Object.Destroy(windEffectObj, 0.5F);
                    });
                    break;
                case Aura.Wind:
                    WindSkill(monster, info, effect);
                    monster.SetAuraFor(Aura.Wind, level, info.auraDuration);
                    DOVirtual.DelayedCall(info.duration + 0.1F, () =>
                    {
                        monster.SetSpeedFor(0.2F, info.duration);
                        var gObj = Object.Instantiate(effect, monster.transform.position, Quaternion.identity);
                        Object.Destroy(gObj, info.duration - 0.5F);
                    });
                    break;
                case Aura.Flame:
                    WindSkill(monster, info, flameEffect);
                    monster.SetAuraFor(Aura.Wind, level, info.auraDuration);
                    monster.GetDamageInevitably(1);
                    burn?.Invoke(info.duration);
                    var burnObj = Object.Instantiate(burnEffect, monster.transform);
                    Object.Destroy(burnObj, info.duration);
                    break;
                case Aura.Ice:
                    WindSkill(monster, info, iceEffect);
                    monster.SetAuraFor(Aura.Wind, level, info.auraDuration);
                    monster.GetDamageInevitably(1);
                    break;
                case Aura.Earth:
                    if (monster.AuraLevel <= level)
                    {
                        WindSkill(monster, info, earthEffect);
                    }
                    break;
            }
        }
    }

    private void WindSkill(Monster monster, AbilityInfo.Info info, GameObject effect)
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
