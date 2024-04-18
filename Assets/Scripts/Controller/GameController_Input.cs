using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public partial class GameController
{
    private const string UI_UNIT_REMOVE = "Btn_UnitRemove";
    private const string UI_UNIT_INFO   = "Btn_Info";
    private const string UI_UNIT_MERGE  = "Btn_Merge";

    // UI Ray
    private GraphicRaycaster    m_raycaster;
    private PointerEventData    m_pointer_event_data;
    private List<RaycastResult> m_ray_results = new List<RaycastResult>();

    // ClickTime
    private float m_click_time = 0.2f;      // 이 시간 보다 빠르게 마우스를 눌럿다 떼야 클릭으로 간주, 이 시간보다 길어지면 프레스
    private float m_click_curr = 0.0f;

    private EInputType m_input_type = EInputType.None;
    public EInputType InputType
    {
        get { return m_input_type; }
        set
        {
            // Press 시작
            if (m_input_type != EInputType.Press && value == EInputType.Press)
                HeroPressStart();

            m_input_type = value;
        }
    }

    public GameObject RayPickUIObject { get { return RayPickUI(); } }

    private void Update_Input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputType = EInputType.None;

            if (RayPickUIObject)
                return;

            // 초기화
            InputInit();

            InputType = EInputType.Click;
        }

        if (Input.GetMouseButton(0))
        {
            if (InputType == EInputType.None)
                return;

            m_click_curr += Time.deltaTime;
            if (m_click_curr > m_click_time)
                InputType = EInputType.Press;

            if (InputType == EInputType.Press)
                HeroPress();
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (InputType)
            {
                case EInputType.Click:
                    HeroClick();
                    break;
                case EInputType.Press:
                    HeroPressEnd();
                    InputInit();
                    break;
            }
        }
    }

    private GameObject RayPickUI()
    {
        if (m_raycaster == null || m_pointer_event_data == null)
            return null;

        m_ray_results.Clear();
        m_pointer_event_data.position = Input.mousePosition;
        m_raycaster.Raycast(m_pointer_event_data, m_ray_results);
        foreach (RaycastResult result in m_ray_results)
            return result.gameObject;

        return null;
    }

    public void InputInit()
    {
        m_click_curr = 0f;
        m_input_type = EInputType.None;
        EndLand = null;

        Managers.UnitCam.gameObject.SetActive(false);

        if (GUI != null)
        {
            GUI.DeleteUIActive(false, 0);
        }

        if (SelectHero != null)
        {
            SelectHero.RangeEffect.Ex_SetActive(false);
            SelectHero = null;
        }

        if (HudHeroInfo != null)
        {
            HudHeroInfo.CloseHeroInfo();
            HudHeroInfo = null;
        }
    }

    #region Click
    private void HeroClick()
    {
        if (RayPickUIObject)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Hero"))
            {
                SelectHero = hit.transform.gameObject.GetComponent<Hero>();
                SelectHero.HudHeroInfo.ShowHeroInfo();
                HudHeroInfo = SelectHero.HudHeroInfo;
                HudHeroInfo.transform.SetAsLastSibling();
            }
        }
    }
    #endregion

    #region Press
    private void HeroPressStart()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Hero"))
            {
                SelectHero = hit.transform.gameObject.GetComponent<Hero>();
                Managers.UnitCam.gameObject.SetActive(true);
                SelectHero.RangeEffect.Ex_SetActive(true);

                SelectHero.OnHeroDrag(true);

                if (GUI != null) 
                    GUI.DeleteUIActive(true, SelectHero.GetHeroData.m_info.m_tier);
            }
        }
    }

    private void HeroPress()
    {
        if (SelectHero == null)
            return;

        // 유닛을 삭제하는 UI 에선 범위를 숨겨주자.
        if (RayPickUIObject && RayPickUIObject.name == UI_UNIT_REMOVE)
            SelectHero.RangeEffect.Ex_SetActive(false);
        else
            SelectHero.RangeEffect.Ex_SetActive(true);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Build")))
        {
            EndLand = LandInfo.Find(x => x.m_trans == hit.transform);
            if (EndLand == null)
                return;

            SelectHero.transform.position = EndLand.m_trans.position + m_hero_position;
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
        {
            SelectHero.transform.position = hit.point;
            EndLand = null;
        }
    }

    private void HeroPressEnd()
    {
        if (SelectHero == null)
            return;

        SelectHero.OnHeroDrag(false);

        // 파는 곳에서 손을 뗏다면 판매
        if (RayPickUIObject && RayPickUIObject.name == UI_UNIT_REMOVE)
        {
            HeroSell();
            return;
        }

        // 타워 설치 구역이 아닌 곳에서 마우스를 놨을 경우
        if (EndLand == null)
        {
            SelectHero.transform.localPosition = m_hero_position;
            return;
        }

        // 놓으려는 곳에 이미 타워가 설치 되어 있는 경우
        if (EndLand.m_build)
        {
            // 내가 원래 있던 위치에 놓은 것이기 때문에 아무것도 처리할 필요가 없다.
            if (EndLand.m_hero == SelectHero)
            {
                SelectHero.transform.localPosition = m_hero_position;
                return;
            }

            // 합성일 경우 여기서 처리 해야됨
            if (EndLand.m_hero.GetHeroData.m_info.m_kind == SelectHero.GetHeroData.m_info.m_kind)
            {
                HeroMerge();
                return;
            }

            // 그것도 아니라면 걍 스왑임
            HeroSwap();
            return;
        }

        // 여기 까지 왔다면 걍 옮기기임,,,
        HeroMove();
    }
    #endregion
}