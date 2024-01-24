using UnityEngine;
using System;
using System.Collections.Generic;

public class UserManager : MonoBehaviour
{
    [Serializable]
    public class TowerInfo
    {
        public int m_kind;      // Ÿ�� ī�ε�
        public int m_level;     // Ÿ�� ����
        public int m_grade;     // Ÿ�� ���

        public TowerInfo(int in_kind, int in_level, int in_grade)
        {
            m_kind = in_kind;
            m_level = in_level;
            m_grade = in_grade;
        }
    }

    private Dictionary<int, TowerInfo> m_dic_have_tower = new Dictionary<int, TowerInfo>();

    // PlayerPrefab ���� ������ ��������
    public void Init()
    {
        m_dic_have_tower.Add(1, new TowerInfo(1, 1, 1));
    }

    public TowerInfo GetUserTowerInfo(int in_kind)
    {
        if (m_dic_have_tower.ContainsKey(in_kind))
            return m_dic_have_tower[in_kind];
        else
            return null;
    }

    /*
     * public static void SaveLocalData<T>(T SaveData, LocalKey Key)
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        binaryFormatter.Serialize(memoryStream, SaveData);
        PlayerPrefs.SetString(Key.ToString(), Convert.ToBase64String(memoryStream.GetBuffer()));
    }

    public static T LoadLocalData<T>(LocalKey key)
    {
        var data = PlayerPrefs.GetString(key.ToString());
        
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream(Convert.FromBase64String(data));

        // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ� ����ϱ� ���� �ٽ� ����Ʈ�� ĳ���� ���ݴϴ�.
        return (T)binaryFormatter.Deserialize(memoryStream);
    }

     if (PlayerPrefs.HasKey(LocalKey.Option.ToString()) == true)
        {
            Account.option = Utility.LoadLocalData<Option>(LocalKey.Option);
            Utility.SaveLocalData<Option>(Account.option, LocalKey.Option);
        }
    */

}
