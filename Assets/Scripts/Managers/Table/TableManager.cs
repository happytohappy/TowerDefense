using UnityEngine;

public partial class TableManager : MonoBehaviour
{
    public void Init()
    {
        InitHeroTable();
        InitMonsterTable();
        InitGachaTable();
    }

    public void Clear()
    {
        ClearHeroTable();
        ClearMonsterTable();
    }
}