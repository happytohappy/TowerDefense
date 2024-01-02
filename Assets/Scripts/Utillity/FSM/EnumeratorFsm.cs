using System.Collections.Generic;
using System.Linq;

public class EnumeratorFsm
{
    private readonly List<EnumeratorState> _states = new List<EnumeratorState>();
    private EnumeratorState _currentState = null;

    public bool AddState(EnumeratorState state)
    {
        if (FindState(state.Key) != null)
        {
            //실패
            return false;
        }

        _states.Add(state);

        return true;
    }

    public void RemoteState(int key)
    {
        var state = FindState(key);
        if (state == null)
            return;

        _states.Remove(state);
    }

    public EnumeratorState FindState(int key)
    {
        return _states.FirstOrDefault(state => state.Key == key);
    }

    public void ClearState()
    {
        _states.Clear();
    }

    public EnumeratorState CurrentState()
    {
        return _currentState;
    }

    public bool ChangeState(int key)
    {
        _currentState?.OnExit?.Invoke();

        _currentState = null;

        var state = FindState(key);
        if (state == null)
            return false;

        _currentState = state;
        _currentState.OnEnter?.Invoke();

        return true;
    }

    public void Update()
    {
        if(_currentState?.OnExecute == null)
            return;

        if (_currentState.IsEnd)
            return;

        if (_currentState.OnExecute.MoveNext() == false)
            _currentState.IsEnd = true;
    }
}