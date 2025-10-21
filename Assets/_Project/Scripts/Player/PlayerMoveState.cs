using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : EntityState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, GameState stateName) : base(player, stateMachine, stateName)
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

        if (_player.MoveX == 0)
            _stateMachine.ChangeState(_player.IdleState);
    }
    #endregion

    #region Private Methods
    #endregion
}