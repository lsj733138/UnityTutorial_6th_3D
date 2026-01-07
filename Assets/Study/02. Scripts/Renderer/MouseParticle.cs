using System;
using UnityEngine;

public class MouseParticle : MonoBehaviour
{
    private ParticleSystem ps;
    private Camera cam;
    
    public int burstCount = 30;
    public float timer = 2f;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        cam = Camera.main;
        
        var mainModule = ps.main;
        mainModule.startLifetime = timer;
        mainModule.gravityModifier = 1f;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;

        if (Input.GetMouseButtonDown(0))
        {
            ps.Emit(burstCount);
        }
    }
}
