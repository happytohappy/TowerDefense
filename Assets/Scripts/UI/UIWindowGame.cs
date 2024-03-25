using UnityEngine;
using TMPro;

public class UIWindowGame : UIWindowBase
{
    [SerializeField] private GameObject m_create;
    [SerializeField] private GameObject m_upgrade;
    [SerializeField] private GameObject m_delete;
    [SerializeField] private GameObject m_next_wave;
    [SerializeField] private TMP_Text m_text_unit_spawn;
    [SerializeField] private TMP_Text m_text_remove_tier;
    [SerializeField] private TMP_Text m_text_remove_energy;

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

        SetEnergy(100);
    }

    public void SetEnergy(int in_energy)
    {
        m_text_unit_spawn.text = $"{in_energy} / {CONST.STAGE_ENERGY_BUY}";
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
        Managers.UI.OpenWindow(WindowID.UIPopupPause);
    }

    public void OnClickWave()
    {
        GameController.GetInstance.MonsterSpawn();
        m_next_wave.SetActive(false);
    }

    public void NextWaveActive()
    {
        m_next_wave.SetActive(true);
    }

    public void OnClickCreateTower()
    {
        GameController.GetInstance.HeroSpawn(ESpawnType.Energy);
    }

    public void OnClickBonusUnit()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupIngameSkill);
    }

    public void OnClickSkill(int in_number)
    {
        InGameSkillParam param = new InGameSkillParam();
        param.m_index = in_number;

        Managers.UI.OpenWindow(WindowID.UIPopupIngameSkill, param);
    }

    //public void OnClickCreate()
    //{
    //    GameController.GetInstance.OnCreate();
    //}

    //public void OnClickUpgrade()
    //{
    //    GameController.GetInstance.OnUpgrade();
    //}

    //public void OnClickDelete()
    //{
    //    GameController.GetInstance.OnDelete();
    //}
}