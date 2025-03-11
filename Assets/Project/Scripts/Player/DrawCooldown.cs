using UnityEngine;
using UnityEngine.UI;

public abstract class DrawCooldown<T> : MonoBehaviour where T : MonoBehaviour, IsCooldownAbility
{
    [SerializeField] private Image _cooldownSkillImage;

    protected T Ability;
    protected float ConvertedFillAmount;

    private void Awake()
    {
        Ability = GetComponent<T>();
    }

    private void OnEnable()
    {
        Ability.OnCooldownChanged += DrawAbilityCooldown;
    }

    private void OnDisable()
    {
        Ability.OnCooldownChanged += DrawAbilityCooldown;
    }

    private void DrawAbilityCooldown(float cooldown)
    {
        ConvertedFillAmount = cooldown / Ability.Cooldown;
        _cooldownSkillImage.fillAmount = ConvertedFillAmount;
    }
}