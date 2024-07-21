using UnityEngine;
using System.Linq;
using TMPro;

public class UIWindowGame : UIWindowBase
{
    private const string SLOT_SYNERGY_PATH = "UI/Item/Slot_Synergy";

    [SerializeField] private GameObject m_delete;
    [SerializeField] private GameObject m_next_wave;
    [SerializeField] private TMP_Text m_text_unit_spawn;
    [SerializeField] private TMP_Text m_text_remove_tier;
    [SerializeField] private TMP_Text m_text_remove_energy;
    [SerializeField] private Transform m_tr_synergy_root;
    [SerializeField] private TMP_Text m_text_round;
    [SerializeField] private TMP_Text m_text_life;
    [SerializeField] private TMP_Text m_text_speed;
    [SerializeField] private TMP_Text m_text_start_round;
    [SerializeField] private Animator m_ani_start_round;
    [SerializeField] private ExtentionButton m_btn_unit_add;

    [Header("튜토리얼")]
    [SerializeField] private GameObject m_go_wave_life = null;
    [SerializeField] private GameObject m_go_summon = null;
    [SerializeField] private GameObject m_go_buff = null;
    [SerializeField] private GameObject m_go_hero = null;
    [SerializeField] private GameObject m_go_mission = null;
    [SerializeField] private GameObject m_go_start = null;

    private int m_curr_wave = 1;
    private int m_max_wave;
    private bool m_buff_tuto = false;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowGame;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        // 최초 꺼져있는 상태
        m_next_wave.Ex_SetActive(false);
        m_btn_unit_add.interactable = true;

        SetEnergy(100);
        SynergyUIClear();

        m_max_wave = Managers.Table.GetWaveCount(Managers.User.SelectStage);
        SetWaveInfo(m_curr_wave);

        m_text_life.Ex_SetText($"{CONST.STAGE_LIFE}/{CONST.STAGE_LIFE}");
        m_text_speed.Ex_SetText($"x{Managers.User.UserData.GameSpeed}");

        CheckTutorial();
    }

    private void CheckTutorial()
    {
        if (Managers.User.UserData.ClearTutorial.Contains(6))
            return;

        // 웨이브 설명
        m_go_wave_life.Ex_SetActive(true);
        Managers.Tutorial.TutorialStart(m_go_wave_life, ETutorialDir.Center, new Vector3(0f, -100f, 0f), "#NONE TEXT 웨이브 설명");
    }

    public void NextTutorial()
    {
        m_buff_tuto = true;
        Managers.Tutorial.TutorialStart(m_go_summon, ETutorialDir.Center, new Vector3(0f, -100f, 0f), "#NONE TEXT 소환 설명");
    }

    public void SetEnergy(int in_energy)
    {
        m_text_unit_spawn.text = $"{in_energy} / {CONST.STAGE_ENERGY_BUY}";
    }

    private void SynergyUIClear()
    {
        for (int i = 1; i < m_tr_synergy_root.childCount; i++)
            Managers.Resource.Destroy(m_tr_synergy_root.GetChild(i).gameObject);
    }

    public void DeleteUIActive(bool in_active, int in_tier)
    {
        if (in_active)
        {
            m_text_remove_tier.Ex_SetText($"Tier {in_tier}");
            switch (in_tier)
            {
                case 1: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_1}"); break;
                case 2: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_2}"); break;
                case 3: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_3}"); break;
                case 4: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_4}"); break;
                case 5: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_5}"); break;
                case 6: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_6}"); break;
                case 7: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_7}"); break;
                case 8: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_8}"); break;
            }
        }

        m_delete.SetActive(in_active);
    }

    public void OnClickPause()
    {
        WaveInfoParam param = new WaveInfoParam();
        param.m_curr_wave = m_curr_wave;
        param.m_max_wave = m_max_wave;

        Managers.UI.OpenWindow(WindowID.UIPopupPause, param);
    }

    public void OnClickWave()
    {
        if (Managers.Tutorial.TutorialProgress)
        {
            m_go_start.Ex_SetActive(false);
            Managers.Tutorial.TutorialEnd();

            Managers.Tutorial.TutorialClear(6);
        }

        Managers.Sound.PlaySFX(AudioEnum.GameStart);

        m_text_start_round.Ex_SetText($"Wave {m_curr_wave}");
        m_ani_start_round.Ex_SetActive(true);
        m_ani_start_round.Ex_Play("Ani_UIWindowGame_Start", this, () =>
        {
            m_ani_start_round.Ex_SetActive(false);
        });

        GameController.GetInstance.MonsterSpawn();
        m_next_wave.SetActive(false);
    }

    public void NextWaveActive()
    {
        m_next_wave.SetActive(true);
    }

    public void SetWaveInfo(int in_curr_wave)
    {
        m_curr_wave = in_curr_wave;

        m_text_round.Ex_SetText($"{string.Format("{0:D2}", in_curr_wave)}/{string.Format("{0:D2}", m_max_wave)}");
    }

    public void SetLifeInfo(int in_curr_life)
    {
        m_text_life.Ex_SetText($"{in_curr_life}/{CONST.STAGE_LIFE}");
    }

    public void OnClickCreateTower()
    {
        if (Managers.Tutorial.TutorialProgress)
        {
            GameController.GetInstance.HeroSpawn(ESpawnType.Tuto);
            Managers.Tutorial.TutorialEnd();

            if (m_buff_tuto == false)
            {
                Managers.Tutorial.TutorialStart(m_go_buff, ETutorialDir.Center, new Vector3(0f, -100f, 0f), "#NONE TEXT 버프 설명");
            }
            else
            {
                // 영웅 넛지
                m_go_hero.Ex_SetActive(true);

                Managers.Tutorial.TutorialStart(m_go_hero);

                var spawnLand = GameController.GetInstance.LandInfo.FindAll(x => x.m_build).ToList();
                var hero = spawnLand.Find(x => x.m_hero.GetHeroData.m_info.m_kind == CONST.TUTORIAL_STAGE_HERO_1);

                var position = Managers.WorldCam.WorldToViewportPoint(hero.m_trans.position);
                var view_position = Managers.UICam.ViewportToWorldPoint(position);
                m_go_hero.transform.position = new Vector3(view_position.x, view_position.y, 0f);
                m_go_hero.transform.localPosition = m_go_hero.transform.localPosition + new Vector3(0f, 50f, 0f);
            }
        }
        else
        {
            GameController.GetInstance.HeroSpawn(ESpawnType.Energy);
        }

        SetUnitEnergy();
    }

    public void OnClickBonusUnit()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupIngameSkill);
    }

    public void OnClickSkill(int in_number)
    {
        if (Managers.Tutorial.TutorialProgress)
            Managers.Tutorial.TutorialEnd();

        InGameSkillParam param = new InGameSkillParam();
        param.m_reward_type = (EADRewardType)in_number;

        Managers.UI.OpenWindow(WindowID.UIPopupIngameSkill, param);
    }

    public void OnClickSynergy()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupSynergy);
    }

    public void OnClickGameSpeed()
    {
        Managers.User.SetGameSpeedUP();

        m_text_speed.Ex_SetText($"x{Managers.User.UserData.GameSpeed}");
        Time.timeScale = Managers.User.UserData.GameSpeed;
    }

    public void OnClickAuto()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupIngameAuto);
    }

    public void OnClickQuest()
    {
        if (Managers.Tutorial.TutorialProgress)
        {
            m_go_mission.Ex_SetActive(false);
            Managers.Tutorial.TutorialEnd();

            m_go_start.Ex_SetActive(true);
            Managers.Tutorial.TutorialStart(m_go_start, ETutorialDir.Center, new Vector3(0f, -100f, 0f), "#NONE TEXT 시작 설명");

            return;
        }

        QuestParam param = new QuestParam();
        param.m_quest_type = EQuestType.Stage;

        Managers.UI.OpenWindow(WindowID.UIPopupQuest, param);
    }

    public void SetUnitEnergy()
    {
        m_btn_unit_add.interactable = GameController.GetInstance.Energy >= CONST.STAGE_ENERGY_BUY;
    }
    
    public void OnCheckHeroSynergy()
    {
        SynergyUIClear();

        var dicSynergy = GameController.GetInstance.GetHeroTypeCount();
        foreach (var e in dicSynergy)
        {
            if (Managers.Table.GetEnableSynergy(e.Key, e.Value) == false)
                continue;

            var slotSynergy = Managers.Resource.Instantiate(SLOT_SYNERGY_PATH, Vector3.zero, m_tr_synergy_root);
            var sc = slotSynergy.GetComponent<Slot_Synergy>();
            sc.SetSynergy(e.Key, e.Value);
        }
    }

    public void OnClickTutoWave()
    {
        m_go_wave_life.Ex_SetActive(false);
        Managers.Tutorial.TutorialEnd();

        Managers.Tutorial.TutorialStart(m_go_summon, ETutorialDir.Center, new Vector3(0f, -100f, 0f), "#NONE TEXT 소환 설명");
    }

    public void OnClickTutoHero()
    {
        m_go_hero.Ex_SetActive(false);
        Managers.Tutorial.TutorialEnd();
    }

    public void OnClickTutoMissionStart()
    {
        m_go_mission.Ex_SetActive(true);
        Managers.Tutorial.TutorialStart(m_go_mission, ETutorialDir.Center, new Vector3(0f, -100f, 0f), "#NONE TEXT 미션 설명");
    }
}