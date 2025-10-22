using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : EntityState
{
    public PlayerWallSlideState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
    {
    }

    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    #endregion

    #region Private Fields
    #endregion

    #region Public Properties
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public override void Update()
    {
        base.Update();

        HandleWallSlide();

        if (_inputs.Player.Jump.WasPressedThisFrame())
            _stateMachine.ChangeState(_player.WallJumpState);

        if (!_player.WallDetected)
            _stateMachine.ChangeState(_player.FallState);

        if (_player.GroundDetected)
        {
            _stateMachine.ChangeState(_player.IdleState);
            _player.FlipX();
        }
    }
    #endregion

    #region Private Methods
    private void HandleWallSlide()
    {
        if (_player.MoveY < 0f)
            _player.SetVelocity(_player.MoveX, _rb.linearVelocityY);
        else
            _player.SetVelocity(_player.MoveX, _rb.linearVelocityY * _player.WallSlideSlowMultiplier);
    }
    #endregion
}