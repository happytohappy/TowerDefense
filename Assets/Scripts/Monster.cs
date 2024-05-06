using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Monster : PawnBase
{
    private const string ANI_RUN = "Run";
    private const string ANI_DIE = "Die";

    [SerializeField] private Transform m_pivot = null;

    private Vector3 m_destination;
    private HPBar m_hp_bar;
    private int m_line_index;
    private SkinnedMeshRenderer[] m_mesh;

    public Transform Pivot => m_pivot;
    public HPBar HPBar => m_hp_bar;
    public List<Transform>   Path                   { get; set; } = new List<Transform>();
    public MonsterInfoData   GetMonsterInfoData     { get; set; } = null;
    public MonsterStatusData GetMonsterStatusData   { get; set; } = null;

    protected override void Start()
    {
        base.Start();

        // 변수 셋팅
        CurrHP    = GetMonsterStatusData.m_hp;
        MaxHP     = GetMonsterStatusData.m_hp;
        m_line_index = 1;       // 0 은 스폰 위치라 1부터 시작

        // 체력바 셋팅
        m_hp_bar = Util.CreateHP(this.transform, CurrHP, MaxHP, new Vector3(0.75f, 0.75f, 1f), new Vector3(0f, 0.8f, 0f), true);

        // 도착지 설정
        SetDestination();

        // 메테리얼 캐싱
        m_mesh = GetComponentsInChildren<SkinnedMeshRenderer>();
        
        // 시작
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

        StartCoroutine(OnDamage());

        if (GetState == FSM_STATE.Die)
        {
            GameController.GetInstance.MonsterKill(this);
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
            // 도착지에 왔다는 뜻
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

        var speedCalculation = Util.GetDeBuffValue(this, EBuff.BUFF_DECREASE_SPEED);
        var speedResult = (int)(GetMonsterStatusData.m_move_speed * speedCalculation);

        this.transform.Translate(Vector3.forward * Time.smoothDeltaTime * speedResult);
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
        SetMaterialsColor(Color.white);

        if (m_hp_bar != null)
            Managers.Resource.Destroy(m_hp_bar.gameObject);

        Managers.Resource.Destroy(this.gameObject);

        GameController.GetInstance.Monsters.Remove(this);
    }

    private IEnumerator OnDamage()
    {
        SetMaterialsColor(Color.red);

        yield return new WaitForSeconds(0.1f);

        SetMaterialsColor(Color.white);
    }

    private void SetMaterialsColor(Color in_color)
    {
        foreach (var mesh in m_mesh)
        {
            foreach (var material in mesh.materials)
            {
                material.color = in_color;
            }
        }
    }
}