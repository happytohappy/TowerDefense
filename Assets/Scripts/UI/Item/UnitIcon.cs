using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour
{
    [SerializeField] private Image m_img_unit = null;
    [SerializeField] private GameObject m_go_star = null;
    [SerializeField] private GameObject m_go_lock = null;
    [SerializeField] private TMP_Text m_text_rarity = null;

    private int m_kind;

    public void SetHaveUnit(int in_kind, string in_rarity)
    {
        // �޾ƿ� �Ķ���ͷ� ���� ������ ���� �ʼ�!
        m_kind = in_kind;
        m_go_star.Ex_SetActive(true);
        m_img_unit.Ex_SetColor(Color.white);
        m_text_rarity.Ex_SetText(in_rarity);
        m_text_rarity.Ex_SetActive(true);
        m_go_lock.Ex_SetActive(false);
    }

    public void SetNoneUnit(int in_kind)
    {
        // �޾ƿ� �Ķ���ͷ� ���� ������ ���� �ʼ�!
        m_kind = in_kind;
        m_go_star.Ex_SetActive(false);
        m_img_unit.Ex_SetColor(Color.black);
        m_text_rarity.Ex_SetActive(false);
        m_go_lock.Ex_SetActive(true);
    }

    public void OnClickHeroInfo()
    {
        var ui = Managers.UI.GetWindow(WindowID.UIPopupUnit, false) as UIPopupUnit;
        if (ui == null)
            return;

        ui.SetUnitInfo(m_kind);
    }
}