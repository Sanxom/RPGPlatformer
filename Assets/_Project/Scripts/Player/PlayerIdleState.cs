using UnityEngine;

public class PlayerIdleState : EntityState
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
    public override void Update()
    {
        base.Update();

        if(_player.MoveX != 0)
        {
            _stateMachine.ChangeState(_player.MoveState);
        }

        if (_player.JumpAction.WasPressedThisFrame())
        {
            Debug.Log("Jumped");
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
