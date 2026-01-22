using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Farm;
using Firebase.Database;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class User
{
    public string UnitList;
    public string ID;
    public string PW;
    public int Gold;
        
    public User() {}

    public User(string UnitList, string ID, string PW, int Gold)
    {
        this.UnitList = UnitList;
        this.ID = ID;
        this.PW = PW;
        this.Gold = Gold;
    }
}

public class IntroManager : MonoBehaviour
{
    private FirebaseDatabase dataBase;
    private DatabaseReference reference;
    private string URL = "https://hellofirebase-63d64-default-rtdb.firebaseio.com/";
    
    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private TMP_InputField pwInput;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button createButton;
    [SerializeField] private Button loginButton;

    private void Start()
    {
        dataBase = FirebaseDatabase.GetInstance(URL);
        reference = dataBase.RootReference;
        
        createButton.onClick.AddListener(() => CreateUserData().Forget());

        loginButton.onClick.AddListener(() => LoginUserData().Forget());
    }

    private async UniTaskVoid CreateUserData()
    {
        string id = idInput.text;
        string pw = pwInput.text;
        if (string.IsNullOrEmpty(id))
        {
            infoText.text = "ID를 입력하세요.";
            await UniTask.Delay(1000);
            ClearText();
            return;
        }
        
        if (string.IsNullOrEmpty(pw))
        {
            infoText.text = "비밀번호를 입력하세요.";
            await UniTask.Delay(1000);
            ClearText();
            return;
        }

        infoText.text = "중복 확인 및 생성 중...";
        SetButtonInteractable(false);

        try
        {
            var snapshot = await reference.Child("UserInfo").OrderByChild("ID").EqualTo(id).GetValueAsync();

            if (snapshot.HasChildren) // 중복 시
            {
                infoText.text = "중복된 아이디입니다.";
                await UniTask.Delay(1000);
                ClearText();
                SetButtonInteractable(true);
                return;
            }

            Dictionary<string, bool> unitDics = new Dictionary<string, bool>();
            
            for (int i = 0; i < 4; i++) // 계정 생성시 캐릭터 초기화
                unitDics.Add($"unit{i}", false);

            string unitListJson = JsonConvert.SerializeObject(unitDics);
            int Gold = 100;

            User newUser = new User(unitListJson, id, pw, Gold);
            string userData = JsonConvert.SerializeObject(newUser);
            await reference.Child("UserInfo").Push().SetRawJsonValueAsync(userData);
            Debug.Log("계정 생성 완료");
            infoText.text = "계정 생성이 완료되었습니다.";
            await UniTask.Delay(1000);
            ClearText();
            SetButtonInteractable(true);

        }
        catch (Exception e)
        {
            Debug.Log(e);
            infoText.text = "오류가 발생했습니다. 다시 시도해주세요.";
            SetButtonInteractable(true);
        }

    }

    #region 계정 로그인
    private async UniTaskVoid LoginUserData()
    {
        string id = idInput.text;
        string pw = pwInput.text;
        if (string.IsNullOrEmpty(id))
        {
            infoText.text = "ID를 입력하세요.";
            await UniTask.Delay(1000);
            SetButtonInteractable(true);
            return;
        }

        SetButtonInteractable(false);
        infoText.text = "로그인 시도 중...";

        try
        {
            var snapshot = await reference.Child("UserInfo").OrderByChild("ID").EqualTo(id).GetValueAsync();
            Debug.Log("1");
            if (snapshot.HasChildren) // ID가 있는 경우
            {
                Debug.Log("2");
                foreach (var child in snapshot.Children)
                {
                    string json = child.GetRawJsonValue();
                    var userData = JsonConvert.DeserializeObject<User>(json);
                    var units = JsonConvert.DeserializeObject<Dictionary<string, bool>>(userData.UnitList);

                    if (pw != userData.PW)
                    {
                        infoText.text = "비밀번호가 틀렸습니다.";
                        await UniTask.Delay(1000);
                        ClearText();
                        SetButtonInteractable(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("로그인 성공");
                        infoText.text = "로그인 성공";
                        DataManager.Instance.SetUserData(userData.ID, userData.Gold, units, child.Key);
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            infoText.text = "오류가 발생하였습니다. 다시 시도해주세요";
            await UniTask.Delay(1000);
            ClearText();
            SetButtonInteractable(true);
        }
    }
    #endregion
    
    private void SetButtonInteractable(bool isActive)
    {
        createButton.interactable = isActive;
        loginButton.interactable = isActive;
    }

    private void ClearText()
    {
        infoText.text = string.Empty;
        idInput.text = string.Empty;
        pwInput.text = string.Empty;
    }
}
