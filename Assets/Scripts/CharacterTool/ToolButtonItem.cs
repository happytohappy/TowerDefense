using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolButtonItem : MonoBehaviour
{
    public Button m_button = null;
    public TMP_Text m_text = null;

    private UIWindowCharacterTool.Asset_Slot m_type;
    private GameObject m_go;
    
    public void SetData(UIWindowCharacterTool.Asset_Slot in_type, GameObject in_go, string in_str)
    {
        m_type = in_type;
        m_go = in_go;

        m_text.text = in_str;
    }

    public void OnClickData()
    {
        switch (m_type)
        {
            // 에셋 1
            case UIWindowCharacterTool.Asset_Slot.A1_Head:
                {
                    var image = m_button.gameObject.GetComponent<Image>();

                    if (m_go.activeInHierarchy)
                    {
                        m_go.Ex_SetActive(false);
                        image.color = Color.white;
                    }
                    else
                    {
                        m_go.Ex_SetActive(true);
                        image.color = Color.green;
                    }
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A1_Body:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_1_body)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A1_Cloak:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_1_cloak)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A1_Right:
                {
                    for (int i = 0; i < CharacterToolController.GetInstance.m_asset_1_right.childCount; i++)
                        Destroy(CharacterToolController.GetInstance.m_asset_1_right.GetChild(i).gameObject);

                    var go = GameObject.Instantiate(m_go);
                    go.transform.SetParent(CharacterToolController.GetInstance.m_asset_1_right);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = Quaternion.identity;
                    go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A1_Left:
                {
                    for (int i = 0; i < CharacterToolController.GetInstance.m_asset_1_left.childCount; i++)
                        Destroy(CharacterToolController.GetInstance.m_asset_1_left.GetChild(i).gameObject);

                    var go = GameObject.Instantiate(m_go);
                    go.transform.SetParent(CharacterToolController.GetInstance.m_asset_1_left);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = Quaternion.identity;
                    go.Ex_SetActive(true);
                }
                break;




            // 에셋 2
            case UIWindowCharacterTool.Asset_Slot.A2_Head:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_2_head)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A2_Body:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_2_body)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A2_Right:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_2_equip_right)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A2_Left:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_2_equip_left)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;



            // 에셋 3
            case UIWindowCharacterTool.Asset_Slot.A3_Head:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_3_head)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A3_Body:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_3_body)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A3_Right:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_3_equip_right)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A3_Left:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_3_equip_left)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;


            // 에셋 4
            case UIWindowCharacterTool.Asset_Slot.A4_Head:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_4_head)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A4_Body:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_4_body)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A4_Right:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_4_equip_right)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset_Slot.A4_Left:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_4_equip_left)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
        }
    }
}
