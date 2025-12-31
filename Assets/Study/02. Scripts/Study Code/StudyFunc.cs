using System;
using System.Collections.Generic;
using UnityEngine;

public class StudyFunc : MonoBehaviour
{
    public Func<int, int, int> myFunc;

    public Func<string, string, bool> myFunc2;

    public List<Func<int, int, int>> funcList = new List<Func<int, int, int>>();
    
    private void Start()
    {
        // myFunc = AddMethod;
        // myFunc2 += CompareText;
        //
        // Debug.Log(myFunc2.Invoke("Hello", "Unity"));
        
        funcList.Add(AddMethod);
        funcList.Add(MinusMethod);
        funcList.Add(MultiplyMethod);

        int result = 0;
        foreach (var func in funcList)
        {
            result += func(10, 3);
        }
        
        Debug.Log(result);
    }

    private int AddMethod(int a, int b)
    {
        return a + b;
    }
    
    private int MinusMethod(int a, int b)
    {
        return a - b;
    }
    
    private int MultiplyMethod(int a, int b)
    {
        return a * b;
    }
    
    public bool CompareText(string a, string b)
    {
        if (a == b)
            return true;

        return false;
    }
}
