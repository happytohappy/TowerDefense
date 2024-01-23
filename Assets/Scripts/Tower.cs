using UnityEngine;

public class Tower : PawnBase
{
    private const string ANI_IDLE   = "Idle";
    private const string ANI_ATTACK = "Attack";

    [SerializeField] private GameObject m_range_effect = null;
    
    private int     m_attack_power = 0;
    private float   m_attack_range = 0.0f;
    private Monster m_target_monster = null;

    public GameObject RangeEffect => m_range_effect;
    public HeroData GetHeroData { get; set; } = null;

    protected override void Start()
    {
        base.Start();

        m_range_effect.Ex_SetActive(false);

        m_attack_power = GetHeroData.m_ATK;
        m_attack_range = GetHeroData.m_attack_range;
        m_range_effect.transform.localScale = new Vector3(m_attack_range, m_attack_range, 1f);

        ChangeState(FSM_STATE.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (m_target_monster != null)
        {
            var dis = Vector3.Distance(m_target_monster.transform.position, this.transform.position);
            if (dis > m_attack_range)
                m_target_monster = null;
            else if (m_target_monster.GetState == FSM_STATE.None || m_target_monster.GetState == FSM_STATE.Die)
                m_target_monster = null;
        }

        if (m_target_monster == null)
        {
            if (NearMonsterSearch())
            {
                ChangeState(FSM_STATE.Attack);
            }
            else
            {
                ChangeState(FSM_STATE.Idle);
            }
        }
    }

    private bool NearMonsterSearch()
    {
        var nearDis = float.MaxValue;
        foreach (var monster in GameController.GetInstance.Monsters)
        {
            if (monster.GetState == FSM_STATE.None || monster.GetState == FSM_STATE.Die)
                continue;

            var dis = Vector3.Distance(monster.transform.position, this.transform.position);
            if (dis > m_attack_range)
                continue;

            if (dis < nearDis)
            {
                nearDis = dis;
                m_target_monster = monster;
            }
        }

        return m_target_monster != null;
    }

    private void OnAttack()
    {
        if (m_target_monster == null)
            return;

        this.transform.LookAt(m_target_monster.transform);
        m_target_monster.OnHit(m_attack_power);
    }

    public override void Enter_Idle()
    {
        m_ani.Play(ANI_IDLE);
    }

    public override void Enter_Attack()
    {
        m_ani.Play(ANI_ATTACK);
    }
}