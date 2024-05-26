using TMPro;
using UnityEngine;
using System;

public class UIPopupCommon : UIWindowBase
{
    [SerializeField] private TMP_Text m_text_contents = null;

    private Action m_callback = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupCommon;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        if (in_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        var param = in_param as CommonInfoParam;
        if (param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        m_text_contents.Ex_SetText(param.m_contents);
        m_callback = param.m_callback;
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void OnClickConfirm()
    {
        m_callback?.Invoke();
        Managers.UI.CloseLast();
    }
}