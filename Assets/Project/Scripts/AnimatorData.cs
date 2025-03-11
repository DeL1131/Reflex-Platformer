using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int MagicBallDestroyAnimation = Animator.StringToHash(nameof(MagicBallDestroyAnimation));
        public static readonly int SwordRunAttack = Animator.StringToHash(nameof(SwordRunAttack));
        public static readonly int StandingSwordAttack = Animator.StringToHash(nameof(StandingSwordAttack));
        public static readonly int AirSwordAttack = Animator.StringToHash(nameof(AirSwordAttack));
        public static readonly int HeroSwordJump = Animator.StringToHash(nameof(HeroSwordJump));
        public static readonly int HeroSmokeJumpEffect = Animator.StringToHash(nameof(HeroSmokeJumpEffect));
        public static readonly int IsHeroRun = Animator.StringToHash(nameof(IsHeroRun));
        public static readonly int IsGround = Animator.StringToHash(nameof(IsGround));
        public static readonly int ZombieDeathAnimation = Animator.StringToHash(nameof(ZombieDeathAnimation));
        public static readonly int ZombieHitAnimation = Animator.StringToHash(nameof(ZombieHitAnimation));
        public static readonly int ZombieAttackAnimation = Animator.StringToHash(nameof(ZombieAttackAnimation));
        public static readonly int SkeletonAttackAnimation = Animator.StringToHash(nameof(SkeletonAttackAnimation));
        public static readonly int SkeletonDeathAnimation = Animator.StringToHash(nameof(SkeletonDeathAnimation));
        public static readonly int SkeletonHitAnimation = Animator.StringToHash(nameof(SkeletonHitAnimation));
    }
}