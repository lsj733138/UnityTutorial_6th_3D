using System;
using System.Collections.Generic;
using UnityEngine;

public class CsvTsvParser : MonoBehaviour
{
    [Serializable]
    public class CharacterData
    {
        public string CharID;
        public string Name;
        public int HP;
        public int Attack;

        public CharacterData(string charID, string Name, int HP, int Attack)
        {
            this.CharID = charID;
            this.Name = Name;
            this.HP = HP;
            this.Attack = Attack;
        }
    }
    
    [SerializeField] private List<CharacterData> characterDatas = new List<CharacterData>();

    #region TSV
    void Start()
    {
        TextAsset dataFile = Resources.Load<TextAsset>("TSVData");

        string data = dataFile.text;
        ParsingData(data);
    }

    private void ParsingData(string data)
    {
        string[] rows = data.Split('\n');

        foreach (string row in rows)
            Debug.Log(row);

        for (int i = 1; i < rows.Length; i++)
        {
            string row = rows[i].Trim(); // 공백 제거

            string[] col = row.Split('\t'); // 탭 기준으로 자르기
            
            CharacterData characterData = new CharacterData(col[0], col[1], int.Parse(col[2]), int.Parse(col[3]));
            characterDatas.Add(characterData);
        }
    }
    #endregion
    
    #region csv
    // void Start()
    // {
    //     TextAsset dataFile = Resources.Load<TextAsset>("CSVData");
    //
    //     string data = dataFile.text;
    //     ParsingData(data);
    // }
    //
    // private void ParsingData(string data)
    // {
    //     string[] rows = data.Split('\n');
    //
    //     foreach (string row in rows)
    //         Debug.Log(row);
    //
    //     for (int i = 1; i < rows.Length; i++)
    //     {
    //         string row = rows[i].Trim(); // 공백 제거
    //
    //         string[] col = row.Split(',');
    //
    //         CharacterData characterData = new CharacterData(col[0], col[1], int.Parse(col[2]), int.Parse(col[3]));
    //         characterDatas.Add(characterData);
    //     }
    // }
    #endregion
    
}
