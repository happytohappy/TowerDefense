using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterToolController : MonoBehaviour
{
    #region 싱글톤
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

    [Header("에셋 1")]
    public Transform m_asset_1_right = null;
    public Transform m_asset_1_left = null;
    public List<GameObject> m_asset_1_head = new List<GameObject>();
    public List<GameObject> m_asset_1_body = new List<GameObject>();
    public List<GameObject> m_asset_1_cloak = new List<GameObject>();
    public List<GameObject> m_asset_1_equip = new List<GameObject>();

    [Header("에셋 2")]
    public List<GameObject> m_asset_2_head = new List<GameObject>();
    public List<GameObject> m_asset_2_body = new List<GameObject>();
    public List<GameObject> m_asset_2_equip_right = new List<GameObject>();
    public List<GameObject> m_asset_2_equip_left = new List<GameObject>();

    [Header("에셋 3")]
    public List<GameObject> m_asset_3_head = new List<GameObject>();
    public List<GameObject> m_asset_3_body = new List<GameObject>();
    public List<GameObject> m_asset_3_equip_right = new List<GameObject>();
    public List<GameObject> m_asset_3_equip_left = new List<GameObject>();

    [Header("에셋 4")]
    public List<GameObject> m_asset_4_head = new List<GameObject>();
    public List<GameObject> m_asset_4_body = new List<GameObject>();
    public List<GameObject> m_asset_4_equip_right = new List<GameObject>();
    public List<GameObject> m_asset_4_equip_left = new List<GameObject>();
}