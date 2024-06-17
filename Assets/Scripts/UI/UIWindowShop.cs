using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIWindowShop : UIWindowBase
{
    [Header("재화")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("스크롤 렉트")]
    [SerializeField] private ScrollRect m_scroll_rect = null;
    [SerializeField] private RectTransform m_view_port = null;
    [SerializeField] private RectTransform m_rect_root = null;
    [SerializeField] private List<RectTransform> m_scroll_item = new List<RectTransform>();

    [Header("탭")]
    [SerializeField] private ParentTab m_parent_tab = null;

    private EShopTab m_tab = EShopTab.Recruit;
    private List<int> m_swipe_pos = new List<int>();
    private Vector2 m_view_port_center;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowShop;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        RefreshUI();

        m_scroll_rect.horizontal = true;
        m_view_port_center = new Vector2(m_view_port.rect.width * 0.5f, m_view_port.rect.height * 0.5f);

        m_swipe_pos.Clear();
        m_swipe_pos.Add(872);
        m_swipe_pos.Add(2341);
        m_swipe_pos.Add(3172);
        m_swipe_pos.Add(3804);
        m_swipe_pos.Add(4635);
        m_swipe_pos.Add(5660);
    }

    private void RefreshUI()
    {
        Util.SetGoods(EGoods.Gold, m_text_gold);
        Util.SetGoods(EGoods.Ruby, m_text_ruby);
        Util.SetGoods(EGoods.Diamond, m_text_diamond);
    }

    public void OnScroll(Vector2 in_vector)
    {
        if (m_scroll_rect.horizontal == false)
            return;

        float minDistance = float.MaxValue;
        int centerIndex = -1;

        for (int i = 0; i < m_scroll_item.Count; ++i)
        {
            var item = m_scroll_item[i];

            // 아이템의 월드 좌표계 중심점 계산
            Vector3 itemWorldPosition = item.TransformPoint(item.rect.center);

            // 아이템의 로컬 좌표계 중심점 계산
            Vector3 itemLocalPosition = m_view_port.InverseTransformPoint(itemWorldPosition);

            // 아이템 중심점과 Viewport 중심점 사이의 거리 계산
            float distance = Vector2.Distance(m_view_port_center, itemLocalPosition);

            // 가장 가까운 아이템 찾기
            if (distance < minDistance)
            {
                minDistance = distance;
                centerIndex = i;
            }
        }

        var newTab = (EShopTab)centerIndex;
        if (m_tab != newTab)
        {
            m_tab = newTab;
            m_parent_tab.SelectTab(centerIndex);
        }
    }

    public void OnClickTab(int index)
    {
        if ((int)m_tab == index)
            return;

        m_tab = (EShopTab)index;

        float targetPositionX = m_swipe_pos[index] - m_view_port.rect.width * 0.5f;
        float scrollX = targetPositionX / (m_rect_root.rect.width - m_view_port.rect.width);
        scrollX = Mathf.Clamp(scrollX, 0f, 1f);
        var targetPos = -scrollX * (m_rect_root.rect.width - m_view_port.rect.width);

        StopAllCoroutines();
        StartCoroutine(LerpScrollView(targetPos));
        m_parent_tab.SelectTab(index);
    }

    private IEnumerator LerpScrollView(float in_target_pos)
    {
        float delta = 0;
        float duration = 1.5f;
        m_scroll_rect.horizontal = false;

        while (delta <= duration)
        {
            float t = delta / duration;

            var xValue = Mathf.Lerp(m_rect_root.anchoredPosition.x, in_target_pos, t);
            m_rect_root.Ex_SetValue(EScrollDir.Horizontal, xValue);

            delta += Time.deltaTime;
            yield return null;
        }

        m_scroll_rect.horizontal = true;
    }

    public void OnClickProbability_Premium()
    {
        ProbabilityParam param = new ProbabilityParam();
        param.m_kind = 2000;

        Managers.UI.OpenWindow(WindowID.UIPopupProbabilityInfo, param);
    }

    public void OnClickProbability_Normal()
    {
        ProbabilityParam param = new ProbabilityParam();
        param.m_kind = 1000;

        Managers.UI.OpenWindow(WindowID.UIPopupProbabilityInfo, param);
    }

    public void OnClickRecruitNormal(int in_count)
    {
        var recruitList = Util.Recruit(ERecruitType.Normal, in_count);
        if (recruitList == null || recruitList.Count == 0)
            return;

        RecruitParam param = new RecruitParam();
        param.m_recruit_type = ERecruitType.Normal;
        param.m_recruit_list.AddRange(recruitList);

        Managers.UI.OpenWindow(WindowID.UIWindowRecruit, param);
    }

    public void OnClickRecruitPremium(int in_count)
    {
        var recruitList = Util.Recruit(ERecruitType.Premium, in_count);
        if (recruitList == null || recruitList.Count == 0)
            return;

        RecruitParam param = new RecruitParam();
        param.m_recruit_type = ERecruitType.Normal;
        param.m_recruit_list.AddRange(recruitList);

        Managers.UI.OpenWindow(WindowID.UIWindowRecruit, param);
    }
}