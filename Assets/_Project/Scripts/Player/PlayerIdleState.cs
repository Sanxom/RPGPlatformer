using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
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
        _player.SetVelocity(0f, _rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if(_player.MoveX != 0)
        {
            _stateMachine.ChangeState(_player.MoveState);
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
