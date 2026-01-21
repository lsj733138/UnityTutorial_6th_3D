using Cysharp.Threading.Tasks;
using Farm;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class User
{
    public string UnitList;
    public string ID;
    public int Gold;
        
    public User() {}

    public User(string UnitList, string ID, int Gold)
    {
        this.UnitList = UnitList;
        this.ID = ID;
        this.Gold = Gold;
    }
}

public class IntroManager : MonoBehaviour
{
    private FirebaseDatabase dataBase;
    private DatabaseReference reference;

    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private TMP_InputField pwInput;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button createButton;
    [SerializeField] private Button loginButton;

    private void Start()
    {
        createButton.onClick.AddListener(() => CreateUserData().Forget());

        loginButton.onClick.AddListener(() => LoginUserData().Forget());
    }

    private async UniTaskVoid CreateUserData()
    {
        
    }

    private async UniTaskVoid LoginUserData()
    {
        
    }
}
