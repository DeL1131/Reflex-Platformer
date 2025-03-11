using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourcePrefab;
    [SerializeField] private int _initialPoolSize = 5;

    private List<AudioSource> _audioSources = new List<AudioSource>();

    private int _maxAudioSources = 10;

    private void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            AddNewAudioSource();
        }
    }

    public void PlaySound(AudioClip clip, AudioMixerGroup output)
    {
        AudioSource audioSource = GetAvailableAudioSource();

        if (audioSource == null)
            return;

        audioSource.outputAudioMixerGroup = output;
        audioSource.clip = clip;
        audioSource.Play();
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (var source in _audioSources)
        {
            if (!source.isPlaying)
                return source;
        }

        if (_audioSources.Count < _maxAudioSources)
        {
            return AddNewAudioSource();
        }

        return null;
    }

    private AudioSource AddNewAudioSource()
    {
        if (_audioSources.Count >= _maxAudioSources)
            return null;

        AudioSource newSource = Instantiate(_audioSourcePrefab, transform);
        _audioSources.Add(newSource);
        return newSource;
    }
}