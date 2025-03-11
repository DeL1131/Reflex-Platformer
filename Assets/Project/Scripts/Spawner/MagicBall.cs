using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour, IDamagable
{
    [SerializeField] private LayerMask _layerMask;

    private float _damage = 25;
    private float _health = 25;
    private float _delayDestroy = 0.2f;

    public event Action<float> Damaged;
    public event Action<MagicBall> OnMagicBallDeactivated;
    public event Action OnMagicBallCollided;

    private void Update()
    {
        if (_health <= 0)
        {
            OnMagicBallCollided?.Invoke();
            StartCoroutine(StartDelayDestroy());
        }
    }

    private void OnEnable()
    {
        _health = 25;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_layerMask == (_layerMask | (1 << collision.gameObject.layer)) && collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
            OnMagicBallCollided?.Invoke();
            StartCoroutine(StartDelayDestroy());
        }

        OnMagicBallCollided?.Invoke();
        StartCoroutine(StartDelayDestroy());
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    private IEnumerator<WaitForSeconds> StartDelayDestroy()
    {
        yield return new WaitForSeconds(_delayDestroy);
        OnMagicBallDeactivated?.Invoke(this);
    }
}