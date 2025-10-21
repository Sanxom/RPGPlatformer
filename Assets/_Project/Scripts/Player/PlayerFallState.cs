using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : EntityState
{
    public PlayerFallState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
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
    }
    #endregion

    #region Private Methods
    #endregion
}