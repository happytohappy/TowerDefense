using System;

public class CFSMState<T>
{
    public virtual int State { get; set; } = 0;

    public Action<T> m_action_enter = null;
    public Action<T> m_action_execute = null;
    public Action<T> m_action_exit = null;
}