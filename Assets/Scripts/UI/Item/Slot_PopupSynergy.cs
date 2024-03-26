using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_PopupSynergy : MonoBehaviour
{
    private const string SLOT_POPUP_SYNERGY_UNIT_ICON_PATH = "UI/Item/UnitIcon";

    [SerializeField] private Transform m_trs_root = null;

    [SerializeField] private Image m_Image_hero_type = null;
    [SerializeField] private TMP_Text m_text_synergy = null;
    [SerializeField] private TMP_Text m_text_synergy_buff = null;

    public void SetData(EHeroType in_type, int in_count)
    {
        m_Image_hero_type.Ex_SetImage(Util.GetUnitType(in_type));

        var synergyList = Managers.Table.GetSynergyInfoDataList(in_type);
        if (synergyList == null)
            return;

        StringBuilder sb = new StringBuilder();
        sb.Append($"{in_type}(");
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

        var synergyData = Managers.Table.GetSynergyInfoData(in_type, in_count);

        m_text_synergy_buff.Ex_SetText("ÀÛ¾÷Áß");

        HashSet<int> useHeroKind = new HashSet<int>();

        var HeroLand = GameController.GetInstance.LandInfo.FindAll(x => x.m_build).ToList();
        foreach (var e in HeroLand)
        {
            if (e.m_hero.GetHeroData.m_info.m_type == in_type)
            {
                if (useHeroKind.Contains(e.m_hero.GetHeroData.m_info.m_kind))
                    continue;

                useHeroKind.Add(e.m_hero.GetHeroData.m_info.m_kind);

                var go = Managers.Resource.Instantiate(SLOT_POPUP_SYNERGY_UNIT_ICON_PATH, Vector3.zero, m_trs_root);
                go.transform.SetAsFirstSibling();

                var userHero = Managers.User.GetUserHeroInfo(e.m_hero.GetHeroData.m_info.m_kind);

                var sc = go.GetComponent<UnitIcon>();
                sc.SetSimpleUnit(e.m_hero.GetHeroData.m_info.m_kind, e.m_hero.GetHeroData.m_info.m_rarity, userHero.m_grade, userHero.m_level);
            }
        }    
    }
}
