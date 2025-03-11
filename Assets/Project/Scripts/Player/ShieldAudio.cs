using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Shield))]

public class ShieldAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _shieldActivateSound;
    [SerializeField] private AudioClip _shieldHitSound;
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private AudioSourcePool _audioSourcePool;

    private Shield _shield;

    private void Awake()
    {
        _shield = GetComponent<Shield>();
    }

    private void Start()
    {
        _shield.ShieldActivated += PlayShieldActivateSound;       
    }

    private void OnEnable()
    {
        _shield.ShieldDamaged += PlayShieldHitSound;
    }

    private void OnDisable()
    {
        _shield.ShieldDamaged -= PlayShieldHitSound;
    }

    private void PlayShieldActivateSound()
    {
        _audioSourcePool.PlaySound(_shieldActivateSound, _mixerGroup);
    }

    private void PlayShieldHitSound()
    {
        _audioSourcePool.PlaySound(_shieldHitSound, _mixerGroup);
    }
}