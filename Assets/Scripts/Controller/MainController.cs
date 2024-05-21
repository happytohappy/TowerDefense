using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainController : MonoBehaviour
{
    private const string TOWN_GOLD  = "Town_Gold";
    private const string TOWN_RUBY  = "Town_Ruby";
    private const string TOWN_DIA   = "Town_Dia";
    private const string TOWN_UNIT  = "Town_Unit";
    private const string TOWN_EQUIP = "Town_Equip";

    // UI Ray
    private GraphicRaycaster m_raycaster;
    private PointerEventData m_pointer_event_data;
    private List<RaycastResult> m_ray_results = new List<RaycastResult>();

    private void Awake()
    {
        m_raycaster = Managers.UICanvas.gameObject.GetComponent<GraphicRaycaster>();
        m_pointer_event_data = new PointerEventData(EventSystem.current);
    }

    private void Start()
    {
        Managers.Sound.PlayBGM(AudioEnum.MainBGM);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RayPickUI())
            {
                Util.CloseToolTip();
                return;
            }

            Ray ray = Managers.WorldCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                bool town = false;
                TownParam param = new TownParam();
                if (hit.transform.CompareTag(TOWN_GOLD))
                {
                    town = true;
                    param.m_town_type = ETownType.Gold;
                }
                else if (hit.transform.CompareTag(TOWN_RUBY))
                {
                    town = true;
                    param.m_town_type = ETownType.Ruby;
                }
                else if (hit.transform.CompareTag(TOWN_DIA))
                {
                    town = true;
                    param.m_town_type = ETownType.Dia;
                }
                else if (hit.transform.CompareTag(TOWN_UNIT))
                {
                    town = true;
                    param.m_town_type = ETownType.Unit;
                }
                else if (hit.transform.CompareTag(TOWN_EQUIP))
                {
                    town = true;
                    param.m_town_type = ETownType.Equip;
                }

                if (town)
                    Managers.UI.OpenWindow(WindowID.UIPopupTown, param);
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
}