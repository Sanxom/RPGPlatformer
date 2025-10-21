using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static EntityState.GameState;

public class Player : MonoBehaviour
{
    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    #endregion

    #region Private Fields
    private PlayerInputActions _input;
    private InputAction _moveAction;
    private StateMachine _stateMachine;
    #endregion

    #region Public Properties
    [Header("Movement")]
    [field: SerializeField] public Vector2 MoveInputVector { get; private set; }
    [field: SerializeField] public float MoveX { get; private set; }
    [field: SerializeField] public float MoveY { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _input = new();
        _moveAction = _input.Player.Movement;
        _stateMachine = new();
        IdleState = new(this, _stateMachine, Idle);
        MoveState = new(this, _stateMachine, Move);
    }

    private void OnEnable()
    {
        _input.Enable();
        _moveAction.performed += ctx => ReadInput(ctx);
        _moveAction.canceled += ctx => ReadInput(ctx);
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
        _input.Disable();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void ReadInput(InputAction.CallbackContext ctx)
    {
        MoveInputVector = ctx.ReadValue<Vector2>();
        MoveX = MoveInputVector.x;
        MoveY = MoveInputVector.y;
    }
    #endregion
}
