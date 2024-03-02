using System;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowCharacterTool : UIWindowBase
{
    public enum Asset1_Slot
    {
        Head,
        Body,
        Cloak,
        Right,
        Left
    }

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

    public void Asset_1_Slot(string in_slot_str)
    {
        var slot = (Asset1_Slot)Enum.Parse(typeof(Asset1_Slot), in_slot_str);

        for (int i = 0; i < m_item_root.childCount; i++)
            Destroy(m_item_root.GetChild(i).gameObject);

        switch (slot)
        {
            case Asset1_Slot.Head:
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_head)
                    CreateItem(slot, e, e.name, e.activeInHierarchy);
                break;
            case Asset1_Slot.Body:
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_body)
                    CreateItem(slot, e, e.name);
                break;
            case Asset1_Slot.Cloak:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_cloak)
                    CreateItem(slot, e, e.name);
                break;
            case Asset1_Slot.Right:
            case Asset1_Slot.Left:
                CreateItem(slot, null, "없음");
                foreach (var e in CharacterToolController.GetInstance.m_asset_1_equip)
                    CreateItem(slot, e, e.name);
                break;
            default:
                break;
        }

        m_item_root.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void CreateItem(Asset1_Slot in_slot, GameObject in_item, string in_name, bool in_color_active = false)
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