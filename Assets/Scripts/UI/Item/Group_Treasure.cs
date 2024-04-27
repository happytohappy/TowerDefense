using UnityEngine;

public class Group_Treasure : MonoBehaviour
{
    private const string SLOT_TREASURE_PATH = "UI/Item/Slot_Treasure";

    [SerializeField] private Transform m_trs_root = null;

    public void SetData()
    {
        var treasures = Managers.Table.GetTreasureInfoAllData();
        foreach (var e in treasures)
        {
            var slotTreasure = Managers.Resource.Instantiate(SLOT_TREASURE_PATH, Vector3.zero, m_trs_root);
            var sc = slotTreasure.GetComponent<Slot_Treasure>();

            sc.SetData(e.m_kind);
        }
    }
}