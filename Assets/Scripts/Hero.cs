using UnityEngine;

public class Hero : PawnBase
{
    private const string ANI_IDLE   = "Idle";
    private const string ANI_ATTACK = "Attack";

    [SerializeField] private GameObject m_range_effect = null;
    
    private Monster m_target_monster = null;
    private Hud_HeroInfo m_hud_hero_info = null;
    private bool m_hero_drag = false;

    public Hud_HeroInfo HudHeroInfo => m_hud_hero_info;
    public GameObject RangeEffect => m_range_effect;
    public HeroData GetHeroData { get; set; } = null;

    public float Range => GetHeroData.m_stat.m_range + Util.GetEquipStat(GetHeroData.m_info.m_kind, EStat.Range);

    protected override void Start()
    {
        base.Start();

        m_hero_drag = false;

        m_range_effect.Ex_SetActive(false);
        m_range_effect.transform.localScale = new Vector3(Range, Range, 1f);

        // 인포 셋팅
        m_hud_hero_info = Util.CreateHudHeroInfo(this, new Vector3(0f, -0.2f, 0f));

        ChangeState(FSM_STATE.Idle);
    }

    protected override void Update()
    {
        // Hero가 Drag 중이라면 아무것도 하지 않는다.
        if (m_hero_drag)
            return;

        base.Update();

        if (m_target_monster != null)
        {
            var dis = Vector3.Distance(m_target_monster.transform.position, this.transform.position);
            if (dis > Range)
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

        if (m_target_monster != null)
        {
            var dir = m_target_monster.transform.position - this.transform.position;
            var quaternion = Quaternion.LookRotation(dir);
            this.transform.eulerAngles = new Vector3(0f, quaternion.eulerAngles.y, 0f);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
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
            if (dis > Range)
                continue;

            if (dis < nearDis)
            {
                nearDis = dis;
                m_target_monster = monster;
            }
        }

        return m_target_monster != null;
    }

    public void OnHeroDrag(bool in_drag)
    {
        m_hero_drag = in_drag;

        if (m_hero_drag)
        {
            m_target_monster = null;
            ChangeState(FSM_STATE.Idle);
        }
    }

    private void OnAttack()
    {
        if (m_target_monster == null)
            return;

        // 기본 공격력
        var atkResult = GetHeroData.m_stat.m_atk;

        // 장비 공격력
        atkResult += (int)Util.GetEquipStat(GetHeroData.m_info.m_kind, EStat.ATK);

        // 버프 통합 공격력
        var atkCalculation = Util.GetBuffValue(GetHeroData, EBuff.BUFF_INCREASE_DAMAGE);
        atkResult = (int)(atkResult * atkCalculation);

        // 랜덤 가중치 적용
        atkResult = (int)(atkResult * UnityEngine.Random.Range(0.8f, 1.2f));

        // 상대 방어력
        atkResult -= m_target_monster.GetMonsterStatusData.m_def;

        // 크리티컬 확률 체크
        // 기본 크리티컬 확률
        var criChanceResult = GetHeroData.m_stat.m_critical_chance;

        // 장비 합산 크리티컬 확률
        criChanceResult += (int)Util.GetEquipStat(GetHeroData.m_info.m_kind, EStat.CriticalChance);

        var criChanceCalculation = Util.GetBuffValue(GetHeroData, EBuff.BUFF_INCREASE_CHANCE);
        criChanceResult = (int)(criChanceResult * criChanceCalculation);

        var ran = UnityEngine.Random.Range(1, 101);
        if (ran <= criChanceResult)
        {
            // 크리 공격력 적용
            // 기본 크리티컬 공격률
            var criDamageResult = GetHeroData.m_skill.m_critical;

            // 장비 합산 크리티컬 공격률
            criDamageResult += (int)Util.GetEquipStat(GetHeroData.m_info.m_kind, EStat.Critical);

            var criDamageCalculation = Util.GetBuffValue(GetHeroData, EBuff.BUFF_INCREASE_CRITICAL);
            criDamageResult = (int)(criDamageResult * criDamageCalculation);

            atkResult = (int)(atkResult * (1 + (criDamageResult * 0.01f)));
        }

        // 최소 데미지 보정
        atkResult = Mathf.Max(atkResult, 1);

        // 근접
        if (string.IsNullOrEmpty(GetHeroData.m_info.m_projectile))
        {
            m_target_monster.OnHit(atkResult);
            Util.CreateHudDamage(m_target_monster.transform.position, Util.CommaText(atkResult));
        }
        // 원거리
        else
        {
            var go = Managers.Resource.Instantiate($"Projectile/{GetHeroData.m_info.m_projectile}");
            var projectile = go.GetComponent<ProjectileBase>();

            projectile.transform.position = this.transform.position;
            projectile.SetData(m_target_monster, atkResult);
        }
    }

    public override void Enter_Idle()
    {
        m_ani.Play(ANI_IDLE);
    }

    public override void Enter_Attack()
    {
        m_ani.speed = Util.GetEquipStat(GetHeroData.m_info.m_kind, EStat.Speed) + Util.GetBuffValue(GetHeroData, EBuff.BUFF_INCREASE_SPEED);
        m_ani.Ex_Play(ANI_ATTACK, this, () =>
        {
            m_ani.speed = 1.0f;
        });
    }
}