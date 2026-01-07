using System;
using UnityEngine;

public class StudyParticle : MonoBehaviour
{
    public ParticleSystem ps;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ps.gameObject.SetActive(true);
        }
        
    }
}
