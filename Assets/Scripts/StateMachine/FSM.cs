using UnityEngine;

public class FSM
{
    BaseState _currentState;
    BaseState _previousState;
    public StateFactory StateFactory;
    public IMoveable _moveable { get; private set; }

    public FSM(IMoveable moveable)
    {
        _moveable = moveable;
        StateFactory = new StateFactory(this);
        _currentState = StateFactory.IdleState;
        Debug.Log(_moveable);
    }

    // Update is called once per frame
    public void UpdateState()
    {
        _currentState.UpdateState();
    }

    private void ChangeState(BaseState StateToChangeTo)
    {
        if (StateToChangeTo == _currentState)
        {
            //Debug.Log($"_currentState: {_currentState} NextState: {StateToChangeTo}");
            _currentState.EnterState();
            return;
        }

        _previousState = _currentState;
        _currentState.ExitState();
        _currentState = StateToChangeTo;
        _currentState.EnterState();
    }

    public bool RequestStateChange(BaseState StateToChangeTo)
    {
        // We want to check to see if we can change states. If we cant then we will return;
        if (_currentState.canTransition || StateToChangeTo == _currentState)
        {
            ChangeState(StateToChangeTo);
            return true;
        }

        return false;
    }

    public void ForceTransition(BaseState StateToChangeTo)
    {
        ChangeState(StateToChangeTo);
    }
}