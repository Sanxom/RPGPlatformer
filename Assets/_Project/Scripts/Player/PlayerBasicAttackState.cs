using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float _attackVelocityTimer;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
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

        GenerateAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (_triggerCalled)
            _stateMachine.ChangeState(_player.IdleState);
    }
    #endregion

    #region Private Methods
    private void HandleAttackVelocity()
    {
        _attackVelocityTimer -= Time.deltaTime;

        if (_attackVelocityTimer < 0)
            _player.SetVelocity(0f, _rb.linearVelocityY);
    }

    private void GenerateAttackVelocity()
    {
        _attackVelocityTimer = _player.AttackVelocityDuration;

        _player.SetVelocity(_player.AttackVelocity.x * _player.FacingDirection, _player.AttackVelocity.y);
    }
    #endregion
}