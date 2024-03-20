using UnityEngine;
using System.Collections.Generic;

public class UIPopupIngameSkill : UIWindowBase
{
    [SerializeField] private List<GameObject> m_list_icon = new List<GameObject>();

    private InGameSkillParam m_param = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupIngameSkill;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        if (in_param == null)
            Managers.UI.CloseLast();

        m_param = in_param as InGameSkillParam;
        if (m_param == null)
            Managers.UI.CloseLast();

        if (m_param.m_index >= m_list_icon.Count)
            Managers.UI.CloseLast();

        for (int i = 0; i < m_list_icon.Count; i++)
        {
            m_list_icon[i].Ex_SetActive(i == m_param.m_index);
        }
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void OnClickAD()
    {
        Managers.AD.ShowAd(SetReward);
    }

    private void SetReward()
    {
        switch (m_param.m_index)
        {
            case 0:
                GameController.GetInstance.HeroSpawn(ESpawnType.AD);
                break;
            case 1:
                // 기획적으로 얼마줄 것인지 정해야 함
                GameController.GetInstance.Energy += 10;
                break;
        }

        OnClickClose();
    }
}
