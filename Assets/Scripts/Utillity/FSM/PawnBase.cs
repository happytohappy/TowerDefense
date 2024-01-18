using UnityEngine;

public abstract class PawnBase : MonoBehaviour
{
    // 기본 변수
    [SerializeField] protected Animator m_ani;

    // FSM 관련 변수
    protected FSM_STATE      m_state;
    protected CFMS<PawnBase> m_FSM;

    // 스탯 관련 변수
    protected int m_curr_hp;
    protected int m_max_hp;

    public virtual FSM_STATE GetState => m_state;

    protected virtual void Start()
    {
        SetFSM();
    }

    protected virtual void SetFSM()
    {
        m_FSM = new CFMS<PawnBase>(this);
        m_FSM.AddState(new FSM.StateIdle<PawnBase>(), true);
        m_FSM.AddState(new FSM.StateRun<PawnBase>());
        m_FSM.AddState(new FSM.StateAttack<PawnBase>());
        m_FSM.AddState(new FSM.StateDie<PawnBase>());
        m_FSM.ChangeDefaultState();
    }

    protected virtual void Update()
    {
        // FSM 업데이트
        m_FSM.Update();
    }

    public virtual void OnHit(int in_damage)
    {
        m_curr_hp -= in_damage;

        if (m_curr_hp <= 0)
            ChangeState(FSM_STATE.Die);
    }

    protected void ChangeState(FSM_STATE in_state, bool in_ignore_same_state = true)
    {
        if (in_ignore_same_state && m_state == in_state)
            return;

        if (GetState == FSM_STATE.Die)
            return;

        m_state = in_state;
        m_FSM.ChangeState((int)m_state);
    }

    #region Virtual FSM
    // Idle
    public virtual void Enter_Idle() { }
    public virtual void Update_Idle() { }
    public virtual void Exit_Idle() { }

    // Run
    public virtual void Enter_Run() { }
    public virtual void Update_Run() { }
    public virtual void Exit_Run() { }

    // Attack
    public virtual void Enter_Attack() { }
    public virtual void Update_Attack() { }
    public virtual void Exit_Attack() { }

    // Death
    public virtual void Enter_Die() { }
    public virtual void Update_Die() { }
    public virtual void Exit_Die() { }
    #endregion
}