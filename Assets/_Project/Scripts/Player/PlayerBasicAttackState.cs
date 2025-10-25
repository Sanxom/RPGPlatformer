using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : EntityState
{

    public PlayerBasicAttackState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
    {
        if (_comboLimit != _player.AttackVelocity.Length)
        {
            _comboLimit = _player.AttackVelocity.Length;
        }
    }

    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    #endregion

    #region Private Fields
    private const string BASIC_ATTACK_ANIM_NAME = "basicAttackIndex";
    private const int FIRST_COMBO_INDEX = 1;

    private float _attackVelocityTimer;
    private float _lastTimeAttacked;
    private int _comboIndex = 1;
    private int _comboLimit = 3;
    #endregion

    #region Public Properties
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public override void Enter()
    {
        base.Enter();
        ResetComboIndex();

        _animator.SetInteger(BASIC_ATTACK_ANIM_NAME, _comboIndex);
        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (_triggerCalled)
            _stateMachine.ChangeState(_player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        _comboIndex++;
        _lastTimeAttacked = Time.time;
    }
    #endregion

    #region Private Methods
    private void HandleAttackVelocity()
    {
        _attackVelocityTimer -= Time.deltaTime;

        if (_attackVelocityTimer < 0)
            _player.SetVelocity(0f, _rb.linearVelocityY);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = _player.AttackVelocity[_comboIndex - 1];
        _attackVelocityTimer = _player.AttackVelocityDuration;

        _player.SetVelocity(attackVelocity.x * _player.FacingDirection, attackVelocity.y);
    }

    private void ResetComboIndex()
    {
        if (_comboIndex > _comboLimit || Time.time > _lastTimeAttacked + _player.ComboResetTime)
            _comboIndex = FIRST_COMBO_INDEX;
    }
    #endregion
}