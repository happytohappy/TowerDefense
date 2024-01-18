using UnityEngine;
using TMPro;

public class UIWindowGame : UIWindowBase
{
    [SerializeField] private TMP_Text m_gold;
    [SerializeField] private GameObject m_create;
    [SerializeField] private GameObject m_upgrade;
    [SerializeField] private GameObject m_delete;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowGame;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    public void SetGold(int in_gold)
    {
        if (in_gold == 0)
            m_gold.text = "0";
        else
            m_gold.text = Util.CommaText(in_gold);
    }

    public void HideButton()
    {
        m_create.SetActive(false);
        m_upgrade.SetActive(false);
        m_delete.SetActive(false);
    }

    public void ShowCreate()
    {
        m_create.SetActive(true);
        m_upgrade.SetActive(false);
        m_delete.SetActive(false);
    }

    public void ShowFixed()
    {
        m_create.SetActive(false);
        m_upgrade.SetActive(true);
        m_delete.SetActive(true);
    }

    public void OnClickPause()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupPause);
    }

    //public void OnClickCreate()
    //{
    //    GameController.GetInstance.OnCreate();
    //}

    //public void OnClickUpgrade()
    //{
    //    GameController.GetInstance.OnUpgrade();
    //}

    //public void OnClickDelete()
    //{
    //    GameController.GetInstance.OnDelete();
    //}
}