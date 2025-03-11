using System;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(SkillShield))]

public class PlayerInput : MonoBehaviour
{
    private const KeyCode CommandJump = KeyCode.Space;
    private const KeyCode CommandAttack = KeyCode.Mouse0;
    private const KeyCode CommandFallMultiplier = KeyCode.S;
    private const KeyCode CommandUseShieldAbility = KeyCode.Alpha1;

    private readonly string Horizontal = "Horizontal";

    private Vector3 _verticalDirection;
    private Vector3 _horizontalDirection;
    private Attacker _attacker;
    private SkillShield _skillShield;
    private GroundDetectionHandler _groundDetectionHandler;

    public event Action SpacePressed;
    public event Action Mouse0Pressed;
    public event Action<Vector3> OnHorizontalDirectionInput;
    public event Action<bool> IsKeyPressed;
    public event Action UseShieldAbility;
    public event Action SpaceReleased;

    private void Awake()
    {
        _skillShield = GetComponent<SkillShield>();
        _attacker = GetComponent<Attacker>();
        _groundDetectionHandler = GetComponent<GroundDetectionHandler>();
    }

    private void Update()
    {
        _horizontalDirection = new Vector2(Input.GetAxis(Horizontal), 0f);
        OnHorizontalDirectionInput?.Invoke(_horizontalDirection);

        if (Input.GetKeyDown(CommandAttack))
        {
            if (_attacker.CurrentCooldown <= 0)
                Mouse0Pressed?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpaceReleased?.Invoke();
        }

        if (Input.GetKeyDown(CommandJump))
        {
            SpacePressed?.Invoke();
        }

        if (Input.GetKeyDown(CommandUseShieldAbility))
        {
            if(_skillShield.CurrentCooldown <= 0)
            UseShieldAbility?.Invoke();
        }

        if (Input.GetKey(CommandFallMultiplier))
        {
            IsKeyPressed?.Invoke(true);
        }
        else
        {
            IsKeyPressed?.Invoke(false);
        }
    }
}