using System;
using UnityEngine;


public class ParticleClass : MonoBehaviour
{
    private void Awake()
    {
        TimerDelegate.onTimerEnd += BoomParticle;
    }

    private void BoomParticle()
    {
        Debug.Log("폭발 이펙트");
    }
}
