public class StateMachine
{
    public EntityState CurrentState { get; private set; }

    public void Init(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void UpdateActiveState()
    {
        CurrentState.Update();
    }
}
