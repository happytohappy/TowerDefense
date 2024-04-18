using UnityEngine;
using System.Collections.Generic;

public class UIPopupIngameSkill : UIWindowBase
{
    [SerializeField] private List<GameObject> m_list_icon = new List<GameObject>();
    [SerializeField] private ExtentionButton m_btn_ad = null;

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

        Time.timeScale = 0f;

        if (in_param == null)
            Managers.UI.CloseLast();

        m_param = in_param as InGameSkillParam;
        if (m_param == null)
            Managers.UI.CloseLast();

        for (int i = 0; i < m_list_icon.Count; i++)
            m_list_icon[i].Ex_SetActive(i == (int)m_param.m_reward_type);

        if (GameController.GetInstance.ADReward.ContainsKey(m_param.m_reward_type))
            m_btn_ad.interactable = GameController.GetInstance.ADReward[m_param.m_reward_type];  
        else
            Managers.UI.CloseLast();
    }

    public override void OnClose()
    {
        Time.timeScale = 1.0f;
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
        GameController.GetInstance.ADReward[m_param.m_reward_type] = false;

        switch (m_param.m_reward_type)
        {
            case EADRewardType.ENERGY:
                GameController.GetInstance.Energy += CONST.STAGE_AD_BONUS_ENERGY;
                break;
            case EADRewardType.LIFE:
                GameController.GetInstance.Life += CONST.STAGE_AD_BONUS_LIFE;
                break;
            case EADRewardType.DAMAGE:
                var damagePer = UnityEngine.Random.Range(CONST.STAGE_AD_ALL_DAMAGE_MIN, CONST.STAGE_AD_ALL_DAMAGE_MAX + 1);

                foreach (var monster in GameController.GetInstance.Monsters)
                {
                    var subtract = (int)(monster.CurrHP * damagePer * 0.01f);
                    monster.OnHit(subtract);
                }
                break;
        }

        OnClickClose();
    }
}
