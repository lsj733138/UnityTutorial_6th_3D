using System;
using UnityEngine;

public class TrailCircle : MonoBehaviour
{
    private TrailRenderer trail;

    public float theta;
    public float timer = 1f;
    public float speed = 3f;
    public float radius = 5f;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        trail.time = timer;
        theta += speed * Time.deltaTime;
        transform.position = radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
    }
}
