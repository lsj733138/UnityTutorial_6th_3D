using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Database;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GoogleDataManager : MonoBehaviour
{
    [Serializable]
    public class CharacterData
    {
        [JsonProperty("ID")] public string characterID;
        [JsonProperty("Name")] public string name;
        [JsonProperty("Hp")] public int hp;
        [JsonProperty("Attack")] public int attack;

        public CharacterData(string characterID, string name, string hp, string attack)
        {
            this.characterID = characterID;
            this.name = name;
            this.hp = int.Parse(hp);
            this.attack = int.Parse(attack);
        }
    }

    private FirebaseDatabase database;
    private DatabaseReference reference;
    
    public string sheetURL;
    public string dbURL;
    
    public List<CharacterData> characterDatas = new List<CharacterData>();

    private void Awake()
    {
        database = FirebaseDatabase.GetInstance(dbURL);
        reference = database.RootReference;
    }

    private void Start()
    {
        SheetToFirebase().Forget();
    }

    public async UniTaskVoid SheetToFirebase() // 스프레드 시트에서 데이터를 받아와 Firebase에 전달
    {
        UnityWebRequest www = UnityWebRequest.Get(sheetURL);
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("데이터 로드 실패");
            return;
        }

        string csvData = www.downloadHandler.text;
        List<CharacterData> data = ParseCSV(csvData);

        string jsonData = JsonConvert.SerializeObject(data);

        await reference.Child("GameData").Child("Characters").SetRawJsonValueAsync(jsonData);
        
        Debug.Log("Firebase업로드 완료");
    }
    
    public List<CharacterData> ParseCSV(string csvData) // sheet에서 csv 받아오고 파싱하는 기능
    {
        Debug.Log(csvData);

        string[] lines = csvData.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            string[] rows = lines[i].Split(",");

            CharacterData newData = new CharacterData(rows[0], rows[1], rows[2], rows[3]);
            characterDatas.Add(newData);
        }

        return characterDatas;
    }
}