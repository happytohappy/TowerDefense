using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolButtonItem : MonoBehaviour
{
    public Button m_button = null;
    public TMP_Text m_text = null;

    private UIWindowCharacterTool.Asset1_Slot m_type;
    private GameObject m_go;
    
    public void SetData(UIWindowCharacterTool.Asset1_Slot in_type, GameObject in_go, string in_str)
    {
        m_type = in_type;
        m_go = in_go;

        m_text.text = in_str;
    }

    public void OnClickData()
    {
        switch (m_type)
        {
            case UIWindowCharacterTool.Asset1_Slot.Head:
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
            case UIWindowCharacterTool.Asset1_Slot.Body:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_1_body)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset1_Slot.Cloak:
                {
                    foreach (var e in CharacterToolController.GetInstance.m_asset_1_cloak)
                        e.Ex_SetActive(false);

                    m_go.Ex_SetActive(true);
                }
                break;
            case UIWindowCharacterTool.Asset1_Slot.Right:
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
            case UIWindowCharacterTool.Asset1_Slot.Left:
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
        }
    }
}
