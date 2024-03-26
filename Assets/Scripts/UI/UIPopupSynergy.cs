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

        var dicSynergy = GameController.GetInstance.GetHeroTypeCount();
        foreach (var e in dicSynergy)
        {
            var go = Managers.Resource.Instantiate(SLOT_POPUP_SYNERGY_PATH, Vector3.zero, m_trs_root);
            var sc = go.GetComponent<Slot_PopupSynergy>();
            sc.SetData(e.Key, e.Value);
        }
    }
}
