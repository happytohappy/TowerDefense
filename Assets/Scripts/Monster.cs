using UnityEngine;
using UnityEngine.AI;

public class Monster : PawnBase
{
    private const string ANI_RUN = "Run";
    private const string ANI_DIE = "Die";

    [SerializeField] private NavMeshAgent m_agent = null;

    private Vector3 m_destination;
    private HPBar m_hp_bar;

    protected override void Start()
    {
        base.Start();

        // 변수 셋팅
        m_curr_hp = 100;
        m_max_hp = 100;

        // 체력바 셋팅
        m_hp_bar = Util.CreateHP(this.transform, m_curr_hp, m_max_hp, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0f, 0.4f, 0f));

        m_agent.enabled = true;
        m_agent.SetDestination(m_destination);

        ChangeState(FSM_STATE.Run);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnHit(int in_damage)
    {
        base.OnHit(in_damage);

        if (GetState == FSM_STATE.Die)
        {
            m_hp_bar.HPBarActive(false);
            return;
        }

        m_hp_bar.HPBarActive(true);
        m_hp_bar.SetHP((float)m_curr_hp / m_max_hp);
    }

    public void SetDestination(Vector3 in_destination)
    {
        m_destination = in_destination;
    }

    public override void Enter_Run()
    {
        m_agent.isStopped = false;

        m_ani.Play(ANI_RUN);
    }

    public override void Update_Run()
    {
        if (m_agent.pathPending)
            return;

        if (m_agent.remainingDistance <= 1.0f)
        {
            Delete();
        }
    }

    public override void Enter_Die()
    {
        //GameController.GetInstance.Gold += 10; 

        m_agent.velocity = Vector3.zero;
        m_agent.isStopped = true;
        m_agent.enabled = false;

        m_ani.Ex_Play(ANI_DIE, this, () =>
        {
            Delete();
        });
    }

    private void Delete()
    {
        ChangeState(FSM_STATE.None);

        if (m_hp_bar != null)
            Managers.Resource.Destroy(m_hp_bar.gameObject);

        Managers.Resource.Destroy(this.gameObject);

        //GameController.GetInstance.Monsters.Remove(this);
    }
}