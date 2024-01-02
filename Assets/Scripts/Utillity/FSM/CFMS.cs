using System.Collections.Generic;

public class CFMS<T> where T : class
{
    private readonly Dictionary<int, CFSMState<T>> m_states        = new Dictionary<int, CFSMState<T>>();

    private CFSMState<T> m_current_state = null;
    private CFSMState<T> m_default_state = null;

    public T Owner { get; protected set; }

    public CFMS(T in_owner)
    {
        Owner = in_owner;
    }

    public void Clear()
    {
        m_states.Clear();
    }

    public bool AddState(CFSMState<T> in_state, bool in_is_default = false)
    {
        if (FindState(in_state.State) != null)
        {
            //실패
            return false;
        }

        m_states.Add(in_state.State, in_state);

        if (true == in_is_default)
        {
            m_default_state = in_state;
        }

        return true;
    }

    public void RemoveState(int in_key)
    {
        if (true == HasState(in_key))
        {
            m_states.Remove(in_key);
        }
    }

    public void RemoveState(CFSMState<T> in_state)
    {
        if (null != in_state)
        {
            RemoveState(in_state.State);
        }
    }

    public CFSMState<T> FindState(int in_key)
    {
        m_states.TryGetValue(in_key, out var function_state);
        return function_state;
    }

    public CFSMState<T> FindState(CFSMState<T> in_state)
    {
        CFSMState<T> result = null;

        do
        {
            if (null == in_state)
            {
                break;
            }
            m_states.TryGetValue(in_state.State, out result);
        } while (false);

        return result;
    }

    public bool HasState(int in_key)
    {
        return m_states.ContainsKey(in_key);
    }

    public CFSMState<T> CurrentState()
    {
        return m_current_state;
    }

    public bool ChangeState(int in_key)
    {
        m_current_state?.m_action_exit?.Invoke(Owner);
        m_current_state = null;

        var state = FindState(in_key);
        if (state == null)
            return false;

        m_current_state = state;
        m_current_state.m_action_enter?.Invoke(Owner);
        
        return true;
    }

    public bool ChangeState(CFSMState<T> in_state)
    {
        return in_state != null && ChangeState(in_state.State);
    }


    public bool ChangeDefaultState()
    {
        return ChangeState(this.m_default_state);
    }

    public void Update()
    {
        m_current_state?.m_action_execute?.Invoke(Owner);
    }
}