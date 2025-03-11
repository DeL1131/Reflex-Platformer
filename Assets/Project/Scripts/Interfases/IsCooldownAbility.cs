using System;

public interface IsCooldownAbility
{
    public event Action<float> OnCooldownChanged;

    public float Cooldown { get; }
}