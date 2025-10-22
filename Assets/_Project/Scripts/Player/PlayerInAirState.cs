using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : EntityState
{
    public PlayerInAirState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
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

        if (_player.MoveX != 0)
        {
            _player.SetVelocity(_player.MoveX * (_player.MoveSpeed * _player.InAirMultiplier), _rb.linearVelocityY);
        }
    }
    #endregion

    #region Private Methods
    #endregion
}