using System.Collections.Generic;
using UnityEngine;

public partial class TableManager : MonoBehaviour
{
    private Dictionary<int, CharacterData> m_dicCharacetrData = new Dictionary<int, CharacterData>();

    private void InitCharacterTable()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/TableCharacter");
        string CSVText = TextFile.text;
        List<string> valueArray = Utility.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            CharacterData CharData = new CharacterData();
            CharData.CharID = int.Parse(words[0]);
            CharData.CharModel = Resources.Load(string.Format("Character/{0}", words[1])) as GameObject;
            CharData.Name = words[2];
            CharData.Grade = int.Parse(words[3]);
            CharData.NameString = int.Parse(words[4]);
            CharData.AttackType = int.Parse(words[5]);
            CharData.Damage = int.Parse(words[6]);
            CharData.Speed = int.Parse(words[7]);
            CharData.CR = int.Parse(words[8]);
            CharData.CRRate = int.Parse(words[9]);
            CharData.DefaultSkill_1 = int.Parse(words[10]);
            CharData.DefaultSkill_1 = int.Parse(words[11]);

            m_dicCharacetrData.Add(CharData.CharID, CharData);
        }
    }

    public CharacterData GetCharData(int CharID)
    {
        if (m_dicCharacetrData.ContainsKey(CharID))
            return m_dicCharacetrData[CharID];
        else
            return null;
    }
}