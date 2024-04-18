using UnityEngine;
using System.Collections.Generic;

public class Monster : PawnBase
{
    private const string ANI_RUN = "Run";
    private const string ANI_DIE = "Die";

    [SerializeField] private Transform m_pivot = null;

    private Vector3 m_destination;
    private HPBar   m_hp_bar;
    private int     m_line_index;

    public Transform Pivot => m_pivot;
    public HPBar HPBar => m_hp_bar;
    public List<Transform>   Path                   { get; set; } = new List<Transform>();
    public MonsterInfoData   GetMonsterInfoData     { get; set; } = null;
    public MonsterStatusData GetMonsterStatusData   { get; set; } = null;

    protected override void Start()
    {
        base.Start();

        // ���� ����
        CurrHP    = GetMonsterStatusData.m_hp;
        MaxHP     = GetMonsterStatusData.m_hp;
        m_line_index = 1;       // 0 �� ���� ��ġ�� 1���� ����

        // ü�¹� ����
        m_hp_bar = Util.CreateHP(this.transform, CurrHP, MaxHP, new Vector3(0.75f, 0.75f, 1f), new Vector3(0f, 0.8f, 0f), true);

        // ������ ����
        SetDestination();

        // ����
        ChangeState(FSM_STATE.Run);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnHit(int in_damage)
    {
        if (GetState == FSM_STATE.Die)
            return;

        base.OnHit(in_damage);

        if (GetState == FSM_STATE.Die)
        {
            GameController.GetInstance.MonsterKill();
            m_hp_bar.HPBarActive(false);
            return;
        }

        m_hp_bar.HPBarActive(true);
        m_hp_bar.SetHP((float)CurrHP / MaxHP);
    }

    private void SetDestination()
    {
        if (m_line_index >= Path.Count)
        {
            // �������� �Դٴ� ��
            GameController.GetInstance.MonsterGoal();
            Delete();
            return;
        }

        this.transform.LookAt(Path[m_line_index]);
        m_destination = Path[m_line_index++].position;
    }

    public override void Enter_Run()
    {
        m_ani.Play(ANI_RUN);
    }

    public override void Update_Run()
    {
        if (Vector3.Distance(this.transform.position, m_destination) <= 0.5f)
        {
            this.transform.position = m_destination;
            SetDestination();
        }

        this.transform.Translate(Vector3.forward * Time.smoothDeltaTime * GetMonsterStatusData.m_move_speed);
    }

    public override void Enter_Die()
    {
        //GameController.GetInstance.Gold += 10; 

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

        GameController.GetInstance.Monsters.Remove(this);
    }
}