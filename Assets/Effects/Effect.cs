using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Effect", menuName = "Content/Effect", order = 0)]
public class Effect : ScriptableObject
{
    public Sprite Icon;
    public enum Category { Buff, Debuff, Other }
    public Category Type = Category.Other;

    [Tooltip("-1 for endless duration")]
    public bool DurationStacks = true;
    public bool ValueStacks = true;

    public EffectBehaviour Behaviour;
}

[System.Serializable]
public struct EffectInstance
{
    public Effect Effect;
    public int Duration;

    public EffectInstance(Effect effect, int duration)
    {
        Effect = effect;
        Duration = duration;
    }
}