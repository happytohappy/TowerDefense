using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;

public class Slot_PopupSynergy : MonoBehaviour
{
    private const string SLOT_POPUP_SYNERGY_UNIT_ICON_PATH = "UI/Item/UnitIcon";

    [SerializeField] private Transform m_trs_root = null;

    [SerializeField] private Image m_Image_hero_type = null;
    [SerializeField] private TMP_Text m_text_synergy = null;
    [SerializeField] private TMP_Text m_text_synergy_buff = null;

    public void SetData(EHeroType in_type, List<SynergyInfoData> in_synergy, int in_count)
    {
        m_Image_hero_type.Ex_SetImage(Util.GetUnitType(in_type));

        // 필요 조건
        StringBuilder sb = new StringBuilder();
        sb.Append($"{in_type} (");
        for (int i = 0; i < in_synergy.Count; i++)
        {
            var synergy = in_synergy[i];
            if (synergy == null)
                continue;

            if (synergy.m_count <= in_count)
                sb.Append("<#FFFFFF>");
            else
                sb.Append("<#9D9D9D>");

            sb.Append(synergy.m_count);

            if (i != in_synergy.Count - 1)
                sb.Append("/");

            sb.Append("</color>");
        }
        sb.Append($")");
        m_text_synergy.Ex_SetText(sb.ToString());

        // 버프 정보
        sb.Clear();
        for (int i = 0; i < in_synergy.Count; i++)
        {
            var synergy = in_synergy[i];
            if (synergy == null)
                continue;

            if (i == 0)
            {
                var buffInfo = Managers.Table.GetBuffInfoData(synergy.m_buff_kind);
                sb.Append($"{buffInfo.m_comment} (");
            }

            if (synergy.m_count <= in_count)
                sb.Append("<#FFFFFF>");
            else
                sb.Append("<#9D9D9D>");

            var buffLevel = Managers.Table.GetBuffLevelData(synergy.m_buff_kind, synergy.m_buff_level);
            sb.Append($"{buffLevel.m_value}%");

            if (i != in_synergy.Count - 1)
                sb.Append("/");

            sb.Append("</color>");
        }
        sb.Append($")");
        m_text_synergy_buff.Ex_SetText(sb.ToString());

        var HeroLand = GameController.GetInstance.LandInfo.FindAll(x => x.m_build).ToList();
        foreach (var e in HeroLand)
        {
            if (e.m_hero.GetHeroData.m_info.m_type == in_type)
            {
                var go = Managers.Resource.Instantiate(SLOT_POPUP_SYNERGY_UNIT_ICON_PATH, Vector3.zero, m_trs_root);
                go.transform.SetAsFirstSibling();

                var sc = go.GetComponent<UnitIcon>();
                sc.SetData(e.m_hero.GetHeroData.m_info.m_kind);
            }
        }    
    }
}