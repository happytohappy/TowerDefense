using UnityEngine;
using UnityEngine.UI;

public class UIPopupSetting : UIWindowBase
{
    [SerializeField] private Slider m_slider_sfx_volum = null;
    [SerializeField] private Slider m_slider_bgm_volum = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupSetting;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        m_slider_sfx_volum.value = Managers.User.UserData.SFXSoundVolum;
        m_slider_bgm_volum.value = Managers.User.UserData.BGMSoundVolum;
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void OnChangeSfxVolum()
    {
        Managers.User.UserData.SFXSoundVolum = m_slider_sfx_volum.value;

        Managers.Sound.SFXVolumChange();
    }

    public void OnChangeBgmVolum()
    {
        Managers.User.UserData.BGMSoundVolum = m_slider_bgm_volum.value;

        Managers.Sound.BGMVolumChange();
    }

    public void OnClickLanguage()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupLanguage);
    }
}
