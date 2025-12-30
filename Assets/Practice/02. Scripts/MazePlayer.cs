using System;
using UnityEngine;


public class MazePlayer : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 moveDir;

    public float moveSpeed = 3f;
    public float rotSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(h, v, 0);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed;

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, moveDir);
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRot, rotSpeed));
        }
    }
}
