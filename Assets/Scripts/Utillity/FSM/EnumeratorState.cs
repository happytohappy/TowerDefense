using System;
using System.Collections;

public class EnumeratorState
{
    public int Key { get; set; } = 0;
    public bool IsEnd = false;

    public Action OnEnter = null;
    public IEnumerator OnExecute;
    public Action OnExit = null;
}