using UnityEngine;
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

    private int m_stage = 1;
    private int m_curr_wave = 1;
    private int m_max_wave;

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
        GameController.GetInstance.HeroSpawn(ESpawnType.Energy);
        SetUnitEnergy();
    }

    public void OnClickBonusUnit()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupIngameSkill);
    }

    public void OnClickSkill(int in_number)
    {
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
}