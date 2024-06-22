using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIWindowShop : UIWindowBase
{
    [Serializable]
    public class RecruitInfo
    {
        public ERecruitType m_recruit_type;
        public TMP_Text m_text_ad_count;
        public GameObject m_go_ad;
        public ExtentionButton m_button_ad;
        public TMP_Text m_text_ad_time;
        public TMP_Text m_recruit_count_1;
        public Image m_recruit_goods_1;
        public TMP_Text m_recruit_price_1;
        public TMP_Text m_recruit_count_2;
        public Image m_recruit_goods_2;
        public TMP_Text m_recruit_price_2;
    }

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

    [Header("Recruit")]
    [SerializeField] private RecruitInfo m_recruit_premium = null;
    [SerializeField] private RecruitInfo m_recruit_nomral = null;

    private EShopTab m_tab = EShopTab.Recruit;
    private List<int> m_swipe_pos = new List<int>();
    private Vector2 m_view_port_center;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowShop;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        if (in_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        var m_param = in_param as ShopParam;
        if (m_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        RefreshGoods();
        RefreshRecruit();

        m_scroll_rect.horizontal = true;
        m_view_port_center = new Vector2(m_view_port.rect.width * 0.5f, m_view_port.rect.height * 0.5f);

        m_swipe_pos.Clear();
        m_swipe_pos.Add(872);
        m_swipe_pos.Add(2341);
        m_swipe_pos.Add(3172);
        m_swipe_pos.Add(3804);
        m_swipe_pos.Add(4635);
        m_swipe_pos.Add(5660);

        OnClickTabQuick((int)m_param.m_tab);
    }

    private void RefreshGoods()
    {
        Util.SetGoods(EGoods.Gold, m_text_gold);
        Util.SetGoods(EGoods.Ruby, m_text_ruby);
        Util.SetGoods(EGoods.Diamond, m_text_diamond);
    }

    private void RefreshRecruit()
    {
        var nowTime = Managers.BackEnd.ServerTimeGetUTCTimeStamp();

        // Premium Recruit
        Managers.User.UserData.Recruit.TryGetValue(ERecruitType.Premium, out var premiumData);
        if (premiumData == null)
            return;

        var premiumTableData_1 = Managers.Table.GetGachaInfoData(ERecruitType.Premium, 1);
        if (premiumTableData_1 == null)
            return;

        var premiumTableData_2 = Managers.Table.GetGachaInfoData(ERecruitType.Premium, 2);
        if (premiumTableData_2 == null)
            return;

        bool premiumAD = nowTime >= premiumData.m_last_reward_time + CONST.AD_PREMIUM_TIME;;
        
        m_recruit_premium.m_text_ad_count.text = $"{premiumData.m_reward_count}/{premiumData.m_reward_max_count}";
        m_recruit_premium.m_button_ad.interactable = premiumAD;
        m_recruit_premium.m_go_ad.Ex_SetActive(!premiumAD);

        m_recruit_premium.m_recruit_count_1.Ex_SetText($"{premiumTableData_1.m_recruit_count}회");
        m_recruit_premium.m_recruit_goods_1.Ex_SetImage(Util.GetResourceImage(premiumTableData_1.m_consumption_kind));
        m_recruit_premium.m_recruit_price_1.Ex_SetText(Util.CommaText(premiumTableData_1.m_consumption_amount));

        m_recruit_premium.m_recruit_count_2.Ex_SetText($"{premiumTableData_2.m_recruit_count}회");
        m_recruit_premium.m_recruit_goods_2.Ex_SetImage(Util.GetResourceImage(premiumTableData_2.m_consumption_kind));
        m_recruit_premium.m_recruit_price_2.Ex_SetText(Util.CommaText(premiumTableData_2.m_consumption_amount));

        // Normal Recruit
        Managers.User.UserData.Recruit.TryGetValue(ERecruitType.Normal, out var normalData);
        if (normalData == null)
            return;

        var normalDataTableData_1 = Managers.Table.GetGachaInfoData(ERecruitType.Normal, 1);
        if (normalDataTableData_1 == null)
            return;

        var normalDataTableData_2 = Managers.Table.GetGachaInfoData(ERecruitType.Normal, 2);
        if (normalDataTableData_2 == null)
            return;

        bool normalAD = nowTime >= normalData.m_last_reward_time + CONST.AD_NORMAL_TIME; ;

        m_recruit_nomral.m_text_ad_count.text = $"{normalData.m_reward_count}/{normalData.m_reward_max_count}";
        m_recruit_nomral.m_button_ad.interactable = normalAD;
        m_recruit_nomral.m_go_ad.Ex_SetActive(!normalAD);

        m_recruit_nomral.m_recruit_count_1.Ex_SetText($"{normalDataTableData_1.m_recruit_count}회");
        m_recruit_nomral.m_recruit_goods_1.Ex_SetImage(Util.GetResourceImage(normalDataTableData_1.m_consumption_kind));
        m_recruit_nomral.m_recruit_price_1.Ex_SetText(Util.CommaText(normalDataTableData_1.m_consumption_amount));

        m_recruit_nomral.m_recruit_count_2.Ex_SetText($"{normalDataTableData_2.m_recruit_count}회");
        m_recruit_nomral.m_recruit_goods_2.Ex_SetImage(Util.GetResourceImage(normalDataTableData_2.m_consumption_kind));
        m_recruit_nomral.m_recruit_price_2.Ex_SetText(Util.CommaText(normalDataTableData_2.m_consumption_amount));
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

    public void OnClickTabQuick(int index)
    {
        if ((int)m_tab == index)
            return;

        m_tab = (EShopTab)index;

        float targetPositionX = m_swipe_pos[index] - m_view_port.rect.width * 0.5f;
        float scrollX = targetPositionX / (m_rect_root.rect.width - m_view_port.rect.width);
        scrollX = Mathf.Clamp(scrollX, 0f, 1f);
        var targetPos = -scrollX * (m_rect_root.rect.width - m_view_port.rect.width);

        StopAllCoroutines();
        m_rect_root.Ex_SetValue(EScrollDir.Horizontal, targetPos);
        m_parent_tab.SelectTab(index);
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

    public void OnClickRecruitNormal(int in_index)
    {
        var recruitData = Managers.Table.GetGachaInfoData(ERecruitType.Normal, in_index);
        if (recruitData == null)
            return;

        var recruitList = Util.Recruit(ERecruitType.Normal, recruitData.m_recruit_count);
        if (recruitList == null || recruitList.Count == 0)
            return;

        RecruitParam param = new RecruitParam();
        param.m_recruit_type = ERecruitType.Normal;
        param.m_recruit_list.AddRange(recruitList);

        Managers.UI.OpenWindow(WindowID.UIWindowRecruit, param);
    }

    public void OnClickRecruitPremium(int in_index)
    {
        var recruitData = Managers.Table.GetGachaInfoData(ERecruitType.Premium, in_index);
        if (recruitData == null)
            return;

        var recruitList = Util.Recruit(ERecruitType.Premium, recruitData.m_recruit_count);
        if (recruitList == null || recruitList.Count == 0)
            return;

        RecruitParam param = new RecruitParam();
        param.m_recruit_type = ERecruitType.Premium;
        param.m_recruit_list.AddRange(recruitList);

        Managers.UI.OpenWindow(WindowID.UIWindowRecruit, param);
    }
}