using System;
using UnityEngine;

public class StudyDelegate : MonoBehaviour
{
    public delegate void Delegate(string s);
    public Delegate onKeyDown;

    public KeyCode keyCode;
    
    private void Start()
    {
        onKeyDown += Respond;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            onKeyDown?.Invoke(keyCode.ToString());
        }
    }

    private void Respond(string s)
    {
        Debug.Log($"{s} 키 누름");
    }
}
