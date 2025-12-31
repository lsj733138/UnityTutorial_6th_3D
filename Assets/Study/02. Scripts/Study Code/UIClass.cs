using System;
using UnityEngine;


public class UIClass : MonoBehaviour
{
    private void Awake()
    {
        TimerDelegate.onTimerStart += StartUI;
        TimerDelegate.onTimerStop += StopUI;
        TimerDelegate.onTimerEnd += EndUI;
        
    }

    private void StartUI()
    {
        Debug.Log("시작 UI On");
    }
    
    private void StopUI()
    {
        Debug.Log("정지 UI On");
    }
    
    private void EndUI()
    {
        Debug.Log("끝 UI On");
    }
}
