using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layerMask;

    private Attacker _attacker;
    private PlayerInput _playerInput;
    private Health _health;

    public event Action<float> Damaged;
    public event Action<float> HealthChanged;
    public event Action Died;

    public LayerMask LayerMask => _layerMask;
    public int Coins { get; private set; }
    public float Damage { get; private set; }
    public float AttackRange { get; private set; }

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _playerInput = GetComponent<PlayerInput>();
        _health = GetComponent<Health>();

        Damage = _damage;
        AttackRange = _attackRange;
    }

    private void OnEnable()
    {       
        _playerInput.Mouse0Pressed += Attack;
        _health.HealthChanged += VerifyHealth;
    }

    private void OnDisable()
    {
        _playerInput.Mouse0Pressed -= Attack;
        _health.HealthChanged -= VerifyHealth;
    }

    public void TakeDamage(float damage)
    {
        Damaged?.Invoke(damage);
    }

    private void VerifyHealth(float currentHealth)
    {
        HealthChanged?.Invoke(currentHealth);

        if (_health.CurrentHealth <= 0)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        _attacker.Attack(LayerMask, AttackRange, Damage);
    }
}