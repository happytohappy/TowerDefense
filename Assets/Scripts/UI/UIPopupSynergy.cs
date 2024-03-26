using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPopupSynergy : UIWindowBase
{
    private const string SLOT_POPUP_SYNERGY_PATH = "UI/Item/Slot_PopupSynergy";

    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private RectTransform m_rect_root = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupSynergy;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        RefreshUI();
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void RefreshUI()
    {
        m_rect_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

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
            var go = Managers.Resource.Instantiate(SLOT_POPUP_SYNERGY_PATH, Vector3.zero, m_trs_root);
            var sc = go.GetComponent<Slot_PopupSynergy>();
            sc.SetData(e.Key, e.Value);
        }
    }
}
