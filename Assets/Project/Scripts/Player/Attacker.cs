using System;
using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour , IsCooldownAbility
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackInterval;

    private Coroutine _coroutineTimerCooldown;

    public event Action Attacked;
    public event Action<float> OnCooldownChanged;

    public float CurrentCooldown { get; private set; } = 0f;
    public float Cooldown { get; private set; } = 2;

    private void FixedUpdate()
    {
        if (CurrentCooldown <= 0 && _coroutineTimerCooldown != null)
        {
            StopCoroutine(_coroutineTimerCooldown);
            _coroutineTimerCooldown = null;
        }
    }

    public void Attack(LayerMask layerMask, float attackRange, float damage)
    {
        if (CurrentCooldown > 0) return;

        Collider2D[] targets = Physics2D.OverlapBoxAll(_attackPoint.position, new Vector2(6, 6), 0, layerMask);

        bool hasHitTarget = false;
        foreach (Collider2D target in targets)
        {
            if (layerMask == (layerMask | (1 << target.gameObject.layer)) && target.gameObject.TryGetComponent(out IDamagable damagable))
            {
                hasHitTarget = true;
                damagable.TakeDamage(damage);
            }
        }

        if (hasHitTarget || targets.Length == 0)
        {
            Attacked?.Invoke();
            CurrentCooldown = _attackInterval;
            if (_coroutineTimerCooldown == null)
                _coroutineTimerCooldown = StartCoroutine(TimerCooldown());
        }
    }

    private IEnumerator TimerCooldown()
    {
        float waitStepInterval = 0.05f;
        WaitForSeconds waitSeconds = new WaitForSeconds(waitStepInterval);

        while (CurrentCooldown > 0)
        {
            CurrentCooldown -= waitStepInterval;
            OnCooldownChanged?.Invoke(CurrentCooldown);
            yield return waitSeconds;
        }

        CurrentCooldown = Cooldown;
        _coroutineTimerCooldown = null;
    }
}
