using Farm;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private TMP_InputField pwInput;

    [SerializeField] private Button createButton;
    [SerializeField] private Button loginButton;

    private void Start()
    {   
        createButton.onClick.AddListener(() =>
        {
            
        });
        
        loginButton.onClick.AddListener((() =>
        {
            DataManager.Instance.UserID = idInput.text;
            SceneManager.LoadScene(1);
        }));
    }
}
