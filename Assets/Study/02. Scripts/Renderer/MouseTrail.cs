using System;
using UnityEngine;

public class MouseTrail : MonoBehaviour
{
    private TrailRenderer trail;

    public float timer = 0.25f;
    public Camera cam;
    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        cam = Camera.main;
    }

    private void Update()
    {
        trail.time = timer;
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 10f;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        if (Input.GetMouseButtonDown(0))
        {
            trail.Clear();
            transform.position = worldPos;
        }
        else if (Input.GetMouseButton(0))
            transform.position = worldPos;
        
    }
}
