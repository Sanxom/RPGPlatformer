using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : EntityState
{
    public PlayerJumpAttackState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
    {
    }

    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    #endregion

    #region Private Fields
    private const string JUMP_ATTACK_TRIGGER_ANIM_NAME = "jumpAttackTrigger";

    private bool _touchedGround;
    #endregion

    #region Public Properties
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public override void Enter()
    {
        base.Enter();

        _touchedGround = false;

        _player.SetVelocity(_player.JumpAttackVelocity.x * _player.FacingDirection, _player.JumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (_player.GroundDetected && !_touchedGround)
        {
            _touchedGround = true;
            _animator.SetTrigger(JUMP_ATTACK_TRIGGER_ANIM_NAME);
            _player.SetVelocity(0f, _rb.linearVelocityY);
        }

        if (_triggerCalled && _player.GroundDetected)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
    }
    #endregion

    #region Private Methods
    #endregion
}