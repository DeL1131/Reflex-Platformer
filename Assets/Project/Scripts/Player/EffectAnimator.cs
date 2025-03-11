using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EffectAnimator : MonoBehaviour
{
    [SerializeField] GroundDetectionHandler _groundDetectionHandler;
    [SerializeField] Mover _mover;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _mover.Jumped += PlayJumpSmokeAnimation;
    }

    private void OnDisable()
    {
        _mover.Jumped -= PlayJumpSmokeAnimation;
    }

    private void PlayJumpSmokeAnimation()
    {
        if (_groundDetectionHandler.IsGround == false)
        {
            EnableSmoke();
           _animator.Play(AnimatorData.Params.HeroSmokeJumpEffect);
        }
    }

    private void EnableSmoke()
    {
        _spriteRenderer.enabled = true;
    }

    private void DisableSmoke()
    {
        _spriteRenderer.enabled = false;
    }
}