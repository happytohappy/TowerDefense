using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_Reward : MonoBehaviour
{
    [SerializeField] private Image m_image_icon = null;
    [SerializeField] private TMP_Text m_text_amount = null;
    [SerializeField] private GameObject m_go_check = null;
    [SerializeField] private TMP_Text m_text_wave = null;
    [SerializeField] private GameObject m_go_lock = null;

    public void Init()
    {
        m_go_check.Ex_SetActive(false);
        m_text_wave.Ex_SetText(string.Empty);
        m_go_lock.Ex_SetActive(false);
    }

    public void SetReward(int in_reward, int in_amount, bool in_check, string in_text)
    {
        Init();

        m_image_icon.Ex_SetImage(Util.GetResourceImage(in_reward));
        m_text_amount.Ex_SetText(Util.CommaText(in_amount));
        m_go_check.Ex_SetActive(in_check);
        m_text_wave.Ex_SetText(in_text);
    }

    public void SetAmount(int in_amount)
    {
        Init();

        if (in_amount > 1)
            m_text_amount.Ex_SetText(Util.CommaText(in_amount));
        else
            m_text_amount.Ex_SetText(string.Empty);
    }

    public void SetLock(bool in_active)
    {
        m_go_lock.Ex_SetActive(in_active);
    }

    public void SetCheck(bool in_active)
    {
        m_go_check.Ex_SetActive(in_active);
    }
}