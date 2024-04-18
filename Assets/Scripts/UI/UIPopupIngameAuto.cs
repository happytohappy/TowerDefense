using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIPopupIngameAuto : UIWindowBase
{
    [Serializable]
    public class AutoToggle
    {
        public EADAutoType m_auto_type;
        public Toggle m_toggle;
    }

    [SerializeField] private GameObject m_go_ad = null;
    [SerializeField] private GameObject m_go_save = null;
    [SerializeField] private List<AutoToggle> m_list_auto = null;

    private Dictionary<EADAutoType, bool> m_ori_data = new Dictionary<EADAutoType, bool>();

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupIngameSkill;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        Time.timeScale = 0f;

        m_ori_data.Clear();
        foreach (var e in m_list_auto)
            m_ori_data.Add(e.m_auto_type, e.m_toggle.isOn);

        RefreshUI();
    }

    private void RefreshUI()
    {
        m_go_ad.Ex_SetActive(!GameController.GetInstance.ADAutoFlag);
        m_go_save.Ex_SetActive(GameController.GetInstance.ADAutoFlag);
    }

    public override void OnClose()
    {
        base.OnClose();

        Time.timeScale = 1f;
    }

    public void OnClickClose()
    {
        foreach (var e in m_list_auto)
            e.m_toggle.isOn = m_ori_data[e.m_auto_type];

        Managers.UI.CloseLast();
    }

    public void OnClickSave()
    {
        foreach (var e in m_list_auto)
            GameController.GetInstance.ADAuto[e.m_auto_type] = e.m_toggle.isOn;

        GameController.GetInstance.AutoAllCheck();

        Managers.UI.CloseLast();
    }

    public void OnClickAD()
    {
        Managers.AD.ShowAd(SetReward);
    }

    private void SetReward()
    {
        GameController.GetInstance.ADAutoFlag = true;

        RefreshUI();
    }
}