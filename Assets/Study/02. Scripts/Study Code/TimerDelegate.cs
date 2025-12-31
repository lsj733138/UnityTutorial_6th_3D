using System;
using System.Collections;
using UnityEngine;


public class TimerDelegate : MonoBehaviour
{
    public delegate void TimerStart();
    public static event TimerStart onTimerStart; // 타이머 시작 이벤트

    public delegate void TimerEnd();
    public static event TimerEnd onTimerEnd; // 타이머 종료 이벤트
    
    public delegate void TimerStop();
    public static event TimerStop onTimerStop; // 타이머 멈춤 이벤트

    public KeyCode keyCode = KeyCode.Space;

    public float timer = 5f;
    public bool isTimer = true;
    
    private void Awake()
    {
        onTimerStart += StartEvent;
        onTimerStop += StopEvent;
        onTimerEnd += EndEvent;
    }

    private void Start()
    {
        onTimerStart?.Invoke();

        StartCoroutine(TimerRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            onTimerStop?.Invoke();
        }
    }

    IEnumerator TimerRoutine()
    {
        while (isTimer)
        {
            Debug.Log($"현재 {timer}초 남았습니다.");
            yield return new WaitForSeconds(1f);
            timer--;

            if (timer <= 0f)
            {
                isTimer = false;
                onTimerEnd?.Invoke();
            }
        }
    }

    private void StartEvent()
    {
        Debug.Log("폭탄이 설치되었습니다.");
    }
    
    private void StopEvent()
    {
        Debug.Log("폭탄이 해체되었습니다..");
        isTimer = false;
        StopAllCoroutines();
    }
    
    private void EndEvent()
    {
        Debug.Log("폭탄이 폭파되었습니다..");
    }
}
