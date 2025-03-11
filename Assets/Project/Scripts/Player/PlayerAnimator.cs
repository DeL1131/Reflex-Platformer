using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(GroundDetectionHandler))]
[RequireComponent(typeof(Attacker))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Mover _mover;
    private PlayerInput _playerInput;
    private GroundDetectionHandler _groundDetectionHandler;
    private Attacker _attacker;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover>();
        _playerInput = GetComponent<PlayerInput>();
        _groundDetectionHandler = GetComponent<GroundDetectionHandler>();
    }

    private void OnEnable()
    {
        _mover.Jumped += PlayJumpAnimation;
        _attacker.Attacked += PlayAttackAnimation;
        _mover.Running += PlayerRunningAnimation;
        _groundDetectionHandler.GroundedChanged += PlayFlyAnimation;
    }

    private void OnDisable()
    {
        _mover.Jumped -= PlayJumpAnimation;
        _attacker.Attacked -= PlayAttackAnimation;
        _mover.Running -= PlayerRunningAnimation;
        _groundDetectionHandler.GroundedChanged -= PlayFlyAnimation;
    }

    private void PlayJumpAnimation()
    {
        _animator.Play(AnimatorData.Params.HeroSwordJump);
    }

    private void PlayAttackAnimation()
    {
        if (_groundDetectionHandler.IsGround && _mover.HorizontalDirection.x == 0)
        {
            _animator.Play(AnimatorData.Params.StandingSwordAttack);
        }
        else if (_groundDetectionHandler.IsGround == false)
        {
            _animator.Play(AnimatorData.Params.AirSwordAttack);
        }
        else if (_groundDetectionHandler.IsGround && _mover.HorizontalDirection.x != 0)
        {
            _animator.Play(AnimatorData.Params.SwordRunAttack);
        }
    }

    private void PlayerRunningAnimation(bool isRunning)
    {
        _animator.SetBool(AnimatorData.Params.IsHeroRun, isRunning);
    }

    private void PlayFlyAnimation(bool isFly)
    {
        _animator.SetBool(AnimatorData.Params.IsGround, isFly);
    }
}