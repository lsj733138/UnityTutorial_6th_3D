using System;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public Path path;

    private Vector3 targetPoint;
    private Vector3 velocity;

    private int currPath;
    private int pathLength;

    public float moveSpeed = 5f;
    public float mass = 5f;

    public bool isLooping = true;

    private void Start()
    {
        pathLength = path.points.Length;
        currPath = 0;

        velocity = transform.forward;
    }

    private void Update()
    {
        targetPoint = path.GetPoint(currPath);
        
        // 목적지 도착
        if (Vector3.Distance(transform.position, targetPoint) < path.radius)
        {
            if (currPath < pathLength - 1)
                currPath++;
            else if (isLooping)
                currPath = 0;
            else
                return;
        }
        
        // 도착을 안했을 시
        if (currPath >= pathLength - 1 && !isLooping)
            velocity += Steer(targetPoint, true);
        else
            velocity += Steer(targetPoint, false);

        transform.position += velocity;
        transform.rotation = Quaternion.LookRotation((velocity));
    }

    private Vector3 Steer(Vector3 target, bool isFinalPoint)
    {
        Vector3 targetDir = target - transform.position;
        float dist = targetDir.magnitude;
        
        targetDir.Normalize();
        Vector3 desiredVelocity;

        if (isFinalPoint && dist < 10f)
            desiredVelocity = moveSpeed * (dist / 10f) * Time.deltaTime * targetDir;
        else
            desiredVelocity = moveSpeed * Time.deltaTime * targetDir;

        Vector3 acceleration = (desiredVelocity - velocity) / mass;

        return acceleration;
    }
}
