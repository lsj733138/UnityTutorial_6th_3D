using System;
using UnityEngine;
using UnityEngine.Events;

public class StudyUnityEvent : MonoBehaviour
{
    public UnityEvent uEvent;
    
    private void Start()
    {
        uEvent.AddListener(MethodA);
    }

    public void MethodA() {}
}
