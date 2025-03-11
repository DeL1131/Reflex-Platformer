using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MagicBall))]

public class MagicBallAnimator : MonoBehaviour
{
    private Animator _animator;
    private MagicBall _magicBall;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _magicBall = GetComponent<MagicBall>();
    }

    private void OnEnable()
    {
        _magicBall.OnMagicBallCollided += PlayDestroyAnimation;
    }

    private void OnDisable()
    {
        _magicBall.OnMagicBallCollided -= PlayDestroyAnimation;
    }

    private void PlayDestroyAnimation()
    {
        _animator.Play(AnimatorData.Params.MagicBallDestroyAnimation);
    }
}