using UnityEngine;

public partial class TableManager : MonoBehaviour
{
    public void Init()
    {
        InitHeroTable();
        InitMonsterTable();
    }

    public void Clear()
    {
        ClearHeroTable();
        ClearMonsterTable();
    }
}