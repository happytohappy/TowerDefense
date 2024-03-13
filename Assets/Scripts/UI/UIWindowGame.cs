using UnityEngine;
using TMPro;

public class UIWindowGame : UIWindowBase
{
    [SerializeField] private TMP_Text m_gold;
    [SerializeField] private GameObject m_create;
    [SerializeField] private GameObject m_upgrade;
    [SerializeField] private GameObject m_delete;
    [SerializeField] private GameObject m_next_wave;
    [SerializeField] private TMP_Text m_txt_next_wave;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowGame;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        // 최초 꺼져있는 상태
        m_next_wave.Ex_SetActive(false);

        SetEnergy(100);
    }

    public void SetEnergy(int in_energy)
    {
        m_txt_next_wave.text = $"{in_energy} / {CONST.STAGE_ENERGY_BUY}";
    }

    public void SetGold(int in_gold)
    {
        if (in_gold == 0)
            m_gold.text = "0";
        else
            m_gold.text = Util.CommaText(in_gold);
    }

    public void ActiveButton(bool in_mouse_down)
    {
        if (in_mouse_down)
        {
            //m_create.SetActive(false);
            //m_upgrade.SetActive(false);
            m_delete.SetActive(true);
        }
        else
        {
            //m_create.SetActive(true);
            //m_upgrade.SetActive(false);
            m_delete.SetActive(false);
        }
    }
    public void OnClickPause()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupPause);
    }

    public void OnClickWave()
    {
        GameController.GetInstance.MonsterSpawn();
        m_next_wave.SetActive(false);
    }

    public void NextWaveActive()
    {
        m_next_wave.SetActive(true);
    }

    public void OnClickCreateTower()
    {
        GameController.GetInstance.HeroSpawn(true);
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