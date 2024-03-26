using UnityEngine;
using System.Linq;
using System.Collections.Generic;
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
        SynergyUIClear();
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

    public void OnClickSynergy()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupSynergy);
    }
    
    public void OnCheckHeroSynergy()
    {
        SynergyUIClear();

        Dictionary<EHeroType, int> DicSynergy = new Dictionary<EHeroType, int>();
        HashSet<int> useHeroKind = new HashSet<int>();

        var HeroLand = GameController.GetInstance.LandInfo.FindAll(x => x.m_build).ToList();
        foreach (var e in HeroLand)
        {
            if (useHeroKind.Contains(e.m_hero.GetHeroData.m_info.m_kind))
                continue;

            useHeroKind.Add(e.m_hero.GetHeroData.m_info.m_kind);

            var heroType = e.m_hero.GetHeroData.m_info.m_type;
            if (DicSynergy.ContainsKey(heroType))
                DicSynergy[heroType]++;
            else
                DicSynergy.Add(heroType, 1);
        }

        foreach (var e in DicSynergy)
        {
            if (Managers.Table.GetEnableSynergy(e.Key, e.Value) == false)
                continue;

            var slotSynergy = Managers.Resource.Instantiate(SLOT_SYNERGY_PATH, Vector3.zero, m_tr_synergy_root);
            var sc = slotSynergy.GetComponent<Slot_Synergy>();
            sc.SetSynergy(e.Key, e.Value);
        }
    }
}