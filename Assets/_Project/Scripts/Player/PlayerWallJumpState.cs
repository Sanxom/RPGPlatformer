using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : EntityState
{
    public PlayerWallJumpState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
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
    public override void Enter()
    {
        base.Enter();

        _player.SetVelocity(_player.WallJumpForce.x * -_player.FacingDirection, _player.WallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (_rb.linearVelocityY < 0f)
            _stateMachine.ChangeState(_player.FallState);

        if (_player.WallDetected)
            _stateMachine.ChangeState(_player.WallSlideState);
    }
    #endregion

    #region Private Methods
    #endregion
}