using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StudyLambda : MonoBehaviour
{
    public delegate void MyDelegate();
    public MyDelegate myDelegate;

    public List<int> numbers = new List<int>();
    public Button[] buttonUIs;
    private void Start()
    {
        myDelegate += () => Debug.Log(10);

        myDelegate?.Invoke();

        #region List
        for (int i = 0; i < 10; i++)
            numbers.Add(i);

        numbers.RemoveAll(n => n % 2 == 0);
        #endregion

        for (int i = 0; i < buttonUIs.Length; i++)
        {
            int j = i;
            buttonUIs[i].onClick.AddListener(() => ButtonEvent2(j));
        }
    }

    private void ButtonEvent()
    {
        
    }

    private void ButtonEvent2(int i)
    {
        
    }
    
    public void OnLog()
    {
        Debug.Log("Hello Unity");
    }
    
}
