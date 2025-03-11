using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class GameMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _clipMenuMusic;
    [SerializeField] private AudioClip _clipGameMusic;
    [SerializeField] private AudioClip _clipGameOverMusic;
    [SerializeField] private Select—omplexityMenu _menu;
    [SerializeField] private GameRestarter _gameRestarter;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _gameRestarter.GameEnded += PlayGameOverMusic;
        _menu.GameStarted += PlayGameMusic;
    }

    private void OnDisable()
    {
        _gameRestarter.GameEnded -= PlayGameOverMusic;
        _menu.GameStarted -= PlayGameMusic;
    }

    private void PlayGameMusic()
    {
        _audioSource.clip = _clipGameMusic;
        _audioSource.Play();
    }

    private void PlayMenuMusic()
    {
        _audioSource.clip = _clipMenuMusic;
        _audioSource.Play();
    }

    private void PlayGameOverMusic()
    {
        _audioSource.clip = _clipGameOverMusic;
        _audioSource.Play();
    }
}