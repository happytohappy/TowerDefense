using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, EquipInfoData> m_dic_equip_info_data = new Dictionary<int, EquipInfoData>();

    private List<int> TempEquip = new List<int>()
    {
        100000,
        100001,
        100002,
        100003,
        100004,
        100005,
        100006,
        100007,
        100008,
        100100,
        100101,
        100102,
        100103,
        100104,
        100105,
        100106,
        100107,
        100108,
        100200,
        100201,
        100202,
        100203,
        100204,
        100205,
        100206,
        100207,
        100208,
        100300,
        100301,
        100302,
        100303,
        100304,
        100305,
        100306,
        100307,
        100308,
        100400,
        100401,
        100402,
        100403,
        100404,
        100405,
        100406,
        100407,
        100408,
        100500,
        100501,
        100502,
        100503,
        100504,
        100505,
        100506,
        100507,
        100508,
        100600,
        100601,
        100602,
        100603,
        100604,
        100605,
        100606,
        100607,
        100608,
        100700,
        100701,
        100702,
        100703,
        100704,
        100705,
        100706,
        100707,
        100708,
    };

    private void InitEquipTable()
    {
        InitEquipInfoTable();
    }

    public EquipInfoData GetEquipInfoData(int in_kind)
    {
        if (m_dic_equip_info_data.ContainsKey(in_kind))
            return m_dic_equip_info_data[in_kind];
        else
            return null;
    }

    public Dictionary<int, EquipInfoData> GetAllEquipInfoData()
    {
        return m_dic_equip_info_data;
    }

    public void TempEquipGacha()
    {
        for (int i = 0; i < 10; i++)
        {
            var ran = UnityEngine.Random.Range(0, TempEquip.Count);
            Managers.User.InsertEquip(TempEquip[ran]);
        }
    }
}