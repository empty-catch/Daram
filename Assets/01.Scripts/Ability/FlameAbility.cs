using System;
using System.Collections.Generic;
using UnityEngine;

public class FlameAbility : IAbility
{
    private Action<int> burn;
    private Sprite sprite;

    public FlameAbility(Action<int> burn, Sprite sprite)
    {
        this.burn = burn;
        this.sprite = sprite;
    }

    public void Execute(List<Monster> activeMonsters, AbilityInfo.Info info)
    {
        for (int i = 0; i < info.hitCount; i++)
        {
            var monster = activeMonsters[i];
            var renderer = new GameObject().AddComponent<SpriteRenderer>();
            renderer.transform.position = monster.transform.position;
            renderer.sprite = sprite;
            burn?.Invoke((int)info.duration);
        }
    }
}
