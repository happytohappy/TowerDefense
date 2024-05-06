using UnityEngine;
using TMPro;

public class Hud_TownInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text m_text_name = null;
    [SerializeField] private GameObject m_go_reward = null;

    private ETownType m_town_type = ETownType.None;
    private Vector3 m_offset = Vector3.zero;

    public Transform Target { get; set; }

    private void FixedUpdate()
    {
        if (Target == null)
            return;

        var position = Managers.WorldCam.WorldToViewportPoint(Target.position);
        var view_position = Managers.UICam.ViewportToWorldPoint(position) + m_offset;
        this.transform.position = new Vector3(view_position.x, view_position.y, 0f);
    }

    public void Set(ETownType in_town_type, Vector3 in_offset)
    {
        m_town_type = in_town_type;

        var townInfo = Managers.Table.GetTownInfoData((int)in_town_type);
        if (townInfo != null)
            m_text_name.Ex_SetText(townInfo.m_title);

        m_offset = in_offset;
    }

    public void RewardActive(bool in_active)
    {
        m_go_reward.Ex_SetActive(in_active);
    }

    public void OnClickReward()
    {
        TownParam param = new TownParam();
        param.m_town_type = m_town_type;

        Managers.UI.OpenWindow(WindowID.UIPopupTown, param);
    }
}