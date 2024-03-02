using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterToolController : MonoBehaviour
{
    #region ½Ì±ÛÅæ
    private static CharacterToolController Instance = null;
    public static CharacterToolController GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType(typeof(CharacterToolController)) as CharacterToolController;

            return Instance;
        }
    }
    #endregion

    [Header("¿¡¼Â 1")]
    public Transform m_asset_1_right = null;
    public Transform m_asset_1_left = null;
    public List<GameObject> m_asset_1_head = new List<GameObject>();
    public List<GameObject> m_asset_1_body = new List<GameObject>();
    public List<GameObject> m_asset_1_cloak = new List<GameObject>();
    public List<GameObject> m_asset_1_equip = new List<GameObject>();

    private void Start()
    {
        Managers.UI.OpenWindow(WindowID.UIWindowCharacterTool);
    }
}