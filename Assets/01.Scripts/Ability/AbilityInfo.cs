using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityInfo", menuName = "Ability Info")]
public class AbilityInfo : ScriptableObject
{
    [SerializeField]
    private Info[] info;

    public Info this[int i] => info[i];

    [Serializable]
    public struct Info
    {
        public int removalCount;
        public int hitCount;
        public int auraDuration;
        public float duration;
        public int manaCost;
        public float cooldown;
    }
}
