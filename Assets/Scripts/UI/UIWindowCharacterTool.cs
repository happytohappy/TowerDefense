using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowCharacterTool : UIWindowBase
{
    public enum Asset_Slot
    {
        A1_Head,
        A1_Body,
        A1_Cloak,
        A1_Right,
        A1_Left,

        A2_Head,
        A2_Body,
        A2_Right,
        A2_Left,

        A3_Head,
        A3_Body,
        A3_Right,
        A3_Left,

        A4_Head,
        A4_Body,
        A4_Right,
        A4_Left
    }

    public List<GameObject> m_model = new List<GameObject>();
    public List<GameObject> m_slots = new List<GameObject>();
    public Transform m_item_root = null;
    public GameObject m_item = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowCharacterTool;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    public void OnClickAsset(int in_index)
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            m_model[i].Ex_SetActive(i == in_index);
            m_slots[i].Ex_SetActive(i == in_index);
        }
    }

    public void Asset_1_Slot(string in_slot_str)
    {
        var slot = (Asset_Slot)Enum.Parse(typeof(Asset_Slot), in_slot_str);

        for (int i = 0; i < m_item_root.childCount; i++)
            Destroy(m_item_root.GetChild(i).gameObject);

        switch (slot)
        {
            // 에셋 1
            case Asset_Slot.A1_Head:
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_head)
                    CreateItem(slot, e, e.name, e.activeInHierarchy);
                break;
            case Asset_Slot.A1_Body:
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_body)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A1_Cloak:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_cloak)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A1_Right:
            case Asset_Slot.A1_Left:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_equip)
                    CreateItem(slot, e, e.name);
                break;

            // 에셋 2
            case Asset_Slot.A2_Head:
                foreach (var e in CharacterToolController.GetInstance.m_asset_2_head)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A2_Body:
                foreach (var e in CharacterToolController.GetInstance.m_asset_2_body)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A2_Right:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_2_equip_right)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A2_Left:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_2_equip_left)
                    CreateItem(slot, e, e.name);
                break;

            // 에셋 3
            case Asset_Slot.A3_Head:
                foreach (var e in CharacterToolController.GetInstance.m_asset_3_head)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A3_Body:
                foreach (var e in CharacterToolController.GetInstance.m_asset_3_body)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A3_Right:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_3_equip_right)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A3_Left:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_3_equip_left)
                    CreateItem(slot, e, e.name);
                break;

            // 에셋 4
            case Asset_Slot.A4_Head:
                foreach (var e in CharacterToolController.GetInstance.m_asset_4_head)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A4_Body:
                foreach (var e in CharacterToolController.GetInstance.m_asset_4_body)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A4_Right:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_4_equip_right)
                    CreateItem(slot, e, e.name);
                break;
            case Asset_Slot.A4_Left:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_4_equip_left)
                    CreateItem(slot, e, e.name);
                break;

            default:
                break;
        }

        m_item_root.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void CreateItem(Asset_Slot in_slot, GameObject in_item, string in_name, bool in_color_active = false)
    {
        var go = GameObject.Instantiate(m_item);
        go.transform.SetParent(m_item_root);
        go.transform.localScale = Vector3.one;

        var image = go.GetComponent<Image>();
        if (in_color_active)
            image.color = Color.green;
        else
            image.color = Color.white;

        var item = go.GetComponent<ToolButtonItem>();
        item.SetData(in_slot, in_item, in_name);
    }
}