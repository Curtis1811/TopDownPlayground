
public abstract class BaseState
{
    protected FSM _fsm;
    public bool canTransition;
    public BaseState(FSM fsm)
    {
        _fsm = fsm;
    }

    public virtual void PrepareState(CharacterContext context)
    {
        // This method can be overridden in derived classes to prepare the state with the given context
    }
    
    public abstract void EnterState();

    public abstract void FixedUpdateState();
   
    public abstract void ExitState();
    
    
}
  