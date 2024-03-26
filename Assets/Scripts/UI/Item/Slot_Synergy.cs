using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Slot_Synergy : MonoBehaviour
{
    [SerializeField] private Image m_img_hero_type = null;
    [SerializeField] private TMP_Text m_text_synergy = null;

    public void SetSynergy(EHeroType in_hero_type, int in_count)
    {
        m_img_hero_type.Ex_SetImage(Util.GetUnitType(in_hero_type));

        var synergyList = Managers.Table.GetSynergyInfoDataList(in_hero_type);
        if (synergyList == null)
            return;

        StringBuilder sb = new StringBuilder();
        sb.Append($"{in_hero_type}(");
        for (int i = 0; i < synergyList.Count; i++)
        {
            var synergy = synergyList[i];
            if (synergy == null)
                continue;

            if (synergy.m_count <= in_count)
                sb.Append("<#FFFFFF>");
            else
                sb.Append("<#9D9D9D>");

            sb.Append(synergy.m_count);

            if (i != synergyList.Count - 1) 
                sb.Append("/");

            sb.Append("</color>");
        }
        sb.Append($")");

        m_text_synergy.Ex_SetText(sb.ToString());
    }
}