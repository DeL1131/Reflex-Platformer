using UnityEngine;

[RequireComponent(typeof(MagicBall))]

public class MagicBallMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private MagicBall _magicBall;

    public float Speed { get; private set; }

    private void Awake()
    {
        _magicBall = GetComponent<MagicBall>();
    }

    private void OnEnable()
    {
        _magicBall.OnMagicBallCollided += DeactivateSpeed;
        Speed = _speed;
    }

    private void OnDisable()
    {
        _magicBall.OnMagicBallCollided -= DeactivateSpeed;
    }

    private void FixedUpdate()
    {
        MoveInLookingDirection();
    }

    private void MoveInLookingDirection()
    {
        Vector3 direction = transform.right;
        Vector3 targetPosition = transform.position + direction;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }

    public void DeactivateSpeed()
    {
        Speed = 0;
    }
}