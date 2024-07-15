using UnityEngine;
using System.Collections.Generic;

public class UIPopupIngameSkill : UIWindowBase
{
    [SerializeField] private List<GameObject> m_list_icon = new List<GameObject>();
    [SerializeField] private ExtentionButton m_btn_ad = null;

    [Header("튜토리얼")]
    [SerializeField] private GameObject m_go_ad = null;

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
        {
            Managers.UI.CloseLast();
            Time.timeScale = 1f;
            return;
        }

        m_param = in_param as InGameSkillParam;
        if (m_param == null)
        {
            Managers.UI.CloseLast();
            Time.timeScale = 1f;
            return;
        }

        for (int i = 0; i < m_list_icon.Count; i++)
            m_list_icon[i].Ex_SetActive(i == (int)m_param.m_reward_type);

        if (GameController.GetInstance.ADReward.ContainsKey(m_param.m_reward_type))
            m_btn_ad.interactable = GameController.GetInstance.ADReward[m_param.m_reward_type];
        else
        { 
            Managers.UI.CloseLast();
            Time.timeScale = 1f;
        }

        CheckTutorial();
    }

    private void CheckTutorial()
    {
        if (Managers.User.UserData.ClearTutorial.Contains(6))
            return;

        // 웨이브 설명
        Managers.Tutorial.TutorialStart(m_go_ad, ETutorialDir.Center, new Vector3(0f, 100f, 0f), "#NONE TEXT 광고 설명");
    }

    public override void OnClose()
    {
        base.OnClose();

        Time.timeScale = 1.0f;
    }

    public void OnClickAD()
    {
        if (Managers.Tutorial.TutorialProgress)
        {
            Managers.Tutorial.TutorialEnd();

            GameController.GetInstance.Energy += CONST.STAGE_AD_BONUS_ENERGY;
            OnClickClose();

            var uiGame = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
            uiGame.NextTutorial();

            return;
        }

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
