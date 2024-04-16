using UnityEngine;
using System.Collections.Generic;

public class UIPopupIngameAuto : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIPopupIngameSkill;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);
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
        //switch (m_param.m_index)
        //{
        //    case 0:
        //        GameController.GetInstance.HeroSpawn(ESpawnType.AD);
        //        break;
        //    case 1:
        //        // 기획적으로 얼마줄 것인지 정해야 함
        //        GameController.GetInstance.Energy += 10;
        //        break;
        //}

        //OnClickClose();
    }
}
