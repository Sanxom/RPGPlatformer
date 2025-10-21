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
    #endregion

    #region Private Fields
    private StateMachine _stateMachine;
    #endregion

    #region Public Properties
    [Header("Movement")]
    [field: SerializeField] public float MoveSpeed { get; private set; }

    public PlayerInputActions Inputs { get; private set; }
    public InputAction MoveAction { get; private set; }
    public InputAction JumpAction { get; private set; }
    public Vector2 MoveInputVector { get; private set; }
    public float MoveX { get; private set; }
    public float MoveY { get; private set; }
    public bool IsFacingRight { get; private set; } = true;

    [Header("References")]
    public Animator PlayerAnimator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
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
        _stateMachine.UpdateActiveState();
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }
    #endregion

    #region Public Methods
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new(xVelocity, yVelocity);
        HandleFlipX(xVelocity);
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

    private void FlipX()
    {
        transform.Rotate(0, 180, 0);
        IsFacingRight = !IsFacingRight;
    }
    #endregion
}
