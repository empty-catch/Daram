using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlameAbility : IAbility
{
    private Action<int> burn;
    private Action<GameObject> destroy;
    private Sprite sprite;

    public FlameAbility(Action<int> burn, Action<GameObject> destroy, Sprite sprite)
    {
        this.burn = burn;
        this.destroy = destroy;
        this.sprite = sprite;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            burn?.Invoke((int)info.duration);
            monster.GetDamage(info.removalCount);

            var renderer = new GameObject().AddComponent<SpriteRenderer>();
            renderer.transform.position = monster.transform.position;
            renderer.sprite = sprite;
            DOVirtual.DelayedCall(0.3F, () => destroy?.Invoke(renderer.gameObject));
        }
    }
}
