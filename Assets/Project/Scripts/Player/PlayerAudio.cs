using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Health))]

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _attackAudioClip;
    [SerializeField] private AudioClip _jumpAudioClip;
    [SerializeField] private AudioClip _hitAudioClip;
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private AudioSourcePool _audioSourcePool;

    private Attacker _attacker;
    private Mover _mover;
    private AudioClip _currentClip;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += PlayHitSound;
        _mover.Jumped += PlayJumpSound;
        _attacker.Attacked += PlayAttackSound;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= PlayHitSound;
        _mover.Jumped -= PlayJumpSound;
        _attacker.Attacked -= PlayAttackSound;
    }

    private void PlayAttackSound()
    {
        if (Time.timeScale != 0)
            _audioSourcePool.PlaySound(_attackAudioClip, _mixerGroup);
    }

    private void PlayJumpSound()
    {
        if (Time.timeScale != 0)
            _audioSourcePool.PlaySound(_jumpAudioClip, _mixerGroup);
    }

    private void PlayHitSound(float damage)
    {
        _audioSourcePool.PlaySound(_hitAudioClip, _mixerGroup);
    }
}