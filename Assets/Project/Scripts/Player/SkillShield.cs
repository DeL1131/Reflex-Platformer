using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Player))]

public class SkillShield : MonoBehaviour, IsCooldownAbility
{
    [SerializeField] private Transform _spawnPointShield;
    [SerializeField] private GameObject _shield;

    private float _currentDuration;
    private float _currentInterval = 0;
    private bool _isAbilityActive = false;
    private bool _isCorotineActive = false;

    private PlayerInput _playerInput;
    private Player _player;
    private Coroutine _corotineAbilityTimerCooldown;

    public event Action<bool> ActiveAbilityChange;
    public event Action<float> OnCooldownChanged;

    public float Cooldown { get; private set; } = 4;
    public float CurrentCooldown { get; private set; } = 0;

    private void Awake()
    {
        CurrentCooldown = Cooldown;
        _corotineAbilityTimerCooldown = StartCoroutine(AbilityTimerCooldown());
        _playerInput = GetComponent<PlayerInput>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _playerInput.UseShieldAbility += ActivetedAbility;
    }

    private void OnDisable()
    {
        _playerInput.UseShieldAbility -= ActivetedAbility;
    }

    private void FixedUpdate()
    {
        if (_currentDuration <= 0)
        {
            _isAbilityActive = DeactivetedAbility();
        }

        if (CurrentCooldown <= 0 && _corotineAbilityTimerCooldown != null)
        {
            StopCoroutine(_corotineAbilityTimerCooldown);
        }
    }

    private void ActivetedAbility()
    {
        _isAbilityActive = true;
        CurrentCooldown = Cooldown;

        ActivateShield();
        StartCoroutine(AbilityTimerDuration(_currentDuration));
        _corotineAbilityTimerCooldown = StartCoroutine(AbilityTimerCooldown());
        ActiveAbilityChange?.Invoke(_isAbilityActive);
    }

    private bool DeactivetedAbility()
    {
        ActiveAbilityChange?.Invoke(_isAbilityActive);
        return false;
    }

    private IEnumerator<WaitForSeconds> AbilityTimerDuration(float duration)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(duration);

        yield return waitForSeconds;
        _currentDuration = 0;
    }

    private IEnumerator<WaitForSeconds> AbilityTimerCooldown()
    {
        float waitStepInterval = 0.05f;

        WaitForSeconds waitSeconds = new WaitForSeconds(waitStepInterval);

        while (CurrentCooldown > 0)
        {
            CurrentCooldown -= waitStepInterval;
            OnCooldownChanged?.Invoke(CurrentCooldown);
            yield return waitSeconds;
        }
    }

    private void ActivateShield()
    {
        _shield.transform.position = _spawnPointShield.position;
        _shield.SetActive(true);
    }
}