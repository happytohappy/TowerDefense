using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Skill : MonoBehaviour
{
    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private Image m_image_skill = null;
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private GameObject m_go_lock = null;

    public Transform GetRoot => m_trs_root;
    
    public string Contents { get; private set; }

    public void SetSkill(int in_skill, int in_level, bool in_lock)
    {
        var buffInfo = Managers.Table.GetBuffInfoData(in_skill);
        if (buffInfo == null)
            return;

        //m_image_skill.Ex_SetImage(Util.GetHeroImage)
        m_text_level.Ex_SetText($"{in_level}");
        m_go_lock.Ex_SetActive(in_lock);

        Contents = "Buff ³»¿ë";
    }
}