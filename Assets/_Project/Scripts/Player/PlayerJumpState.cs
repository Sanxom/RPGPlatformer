using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerInAirState
{
    public PlayerJumpState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
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

        _player.SetVelocity(_rb.linearVelocityX, _player.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (_rb.linearVelocityY < 0)
            _stateMachine.ChangeState(_player.FallState);
    }
    #endregion

    #region Private Methods
    #endregion
}