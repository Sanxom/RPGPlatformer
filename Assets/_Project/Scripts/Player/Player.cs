using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static EntityState.EnumState;

public class Player : MonoBehaviour
{
    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    [Header("Collision Detection")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    #endregion

    #region Private Fields
    private StateMachine _stateMachine;
    #endregion

    #region Public Properties
    [field: SerializeField, Header("Attack")] public Vector2[] AttackVelocity { get; private set; }
    [field: SerializeField] public float AttackVelocityDuration { get; private set; } = 0.1f;
    [field: SerializeField] public float ComboResetTime { get; private set; } = 1f;

    [field: SerializeField, Header("Movement")] public Vector2 WallJumpForce { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float InAirMultiplier { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float WallSlideSlowMultiplier { get; private set; } = 0.3f;
    [field: SerializeField, Space] public float DashDuration { get; private set; } = 0.25f;
    [field: SerializeField] public float DashSpeed { get; private set; } = 20f;

    public Vector2 MoveInputVector { get; private set; }
    public float MoveX { get; private set; }
    public float MoveY { get; private set; }
    public int FacingDirection { get; private set; } = 1;
    public bool IsFacingRight { get; private set; } = true;

    [Header("Input")]
    public PlayerInputActions Inputs { get; private set; }
    public InputAction MoveAction { get; private set; }
    public InputAction JumpAction { get; private set; }

    [Header("References")]
    public Animator PlayerAnimator { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    [Header("State Machine")]
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerBasicAttackState BasicAttackState { get; private set; }

    public bool GroundDetected { get; private set; }
    public bool WallDetected { get; private set; }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Inputs = new();
        MoveAction = Inputs.Player.Movement;
        JumpAction = Inputs.Player.Jump;

        _stateMachine = new();
        IdleState = new(this, _stateMachine, Idle);
        MoveState = new(this, _stateMachine, Move);
        JumpState = new(this, _stateMachine, JumpFall);
        FallState = new(this, _stateMachine, JumpFall);
        WallSlideState = new(this, _stateMachine, WallSlide);
        WallJumpState = new(this, _stateMachine, JumpFall);
        DashState = new(this, _stateMachine, Dash);
        BasicAttackState = new(this, _stateMachine, BasicAttack);
    }

    private void OnEnable()
    {
        Inputs.Enable();
        MoveAction.performed += ctx => ReadInput(ctx);
        MoveAction.canceled += ctx => ReadInput(ctx);
    }

    private void Start()
    {
        _stateMachine.Init(IdleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        _stateMachine.UpdateActiveState();
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * FacingDirection, 0f));
    }
    #endregion

    #region Public Methods
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new(xVelocity, yVelocity);
        HandleFlipX(xVelocity);
    }

    public void FlipX()
    {
        transform.Rotate(0, 180, 0);
        IsFacingRight = !IsFacingRight;
        FacingDirection *= -1;
    }

    public void CallAnimationTrigger()
    {
        _stateMachine.CurrentState.CallAnimationTrigger();
    }
    #endregion

    #region Private Methods
    private void ReadInput(InputAction.CallbackContext ctx)
    {
        MoveInputVector = ctx.ReadValue<Vector2>();
        MoveX = MoveInputVector.x;
        MoveY = MoveInputVector.y;
    }

    private void HandleFlipX(float xVelocity)
    {
        switch (xVelocity)
        {
            case > 0 when !IsFacingRight:
            case < 0 when IsFacingRight:
                FlipX();
                break;
        }
    }

    private void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        WallDetected = Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, wallCheckDistance, whatIsGround);
    }
    #endregion
}
