using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    #endregion

    #region Private Fields
    private Player _player;
    #endregion

    #region Public Properties
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }
    #endregion

    #region Public Methods
    private void CurrentStateTrigger()
    {
        _player.CallAnimationTrigger();
    }
    #endregion

    #region Private Methods
    #endregion
}