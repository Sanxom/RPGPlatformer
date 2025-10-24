using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : EntityState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
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

        if (_rb.linearVelocityY < 0f && !_player.GroundDetected)
        {
            _stateMachine.ChangeState(_player.FallState);
        }

        if (_inputs.Player.Jump.WasPressedThisFrame())
            _stateMachine.ChangeState(_player.JumpState);

        if (_inputs.Player.Attack.WasPressedThisFrame())
        {
            _stateMachine.ChangeState(_player.BasicAttackState);
        }
    }
    #endregion

    #region Private Methods
    #endregion
}