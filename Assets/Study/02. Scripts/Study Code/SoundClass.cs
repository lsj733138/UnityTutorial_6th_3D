using UnityEngine;


public class SoundClass : MonoBehaviour
{
    private void Awake()
    {
        TimerDelegate.onTimerStart += StartSound;
        TimerDelegate.onTimerStop += StopSound;
        TimerDelegate.onTimerEnd += EndSound;
    }

    private void StartSound()
    {
        Debug.Log("시작 사운드");
    }

    public void StopSound()
    {
        Debug.Log("해체 사운드");
    }
    
    private void EndSound()
    {
        Debug.Log("터지는 사운드");
    }
}
