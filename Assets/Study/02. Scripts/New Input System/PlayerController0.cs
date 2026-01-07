using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController0 : MonoBehaviour
{
    private CharacterController cc;

    private Vector2 moveInput;
    public float moveSpeed = 5f;
    private Vector3 verticalVelocity;
    
    public InputActionAsset inputAsset;

    public InputAction move;
    public InputAction attack;
    public InputAction jump;
    public InputAction interaction;
    
    private float rotSpeed = 10f;
    
    public float gravity = -30f;
    public float jumpPower = 10f;
    
    private void Start()
    {
        move = inputAsset.actionMaps[0].actions[0];
        attack = inputAsset.actionMaps[0].actions[1];
        jump = inputAsset.actionMaps[0].actions[2];
        interaction = inputAsset.actionMaps[0].actions[3];
        
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        moveInput = move.ReadValue<Vector2>();
        Debug.Log("Move : " + moveInput);

        Vector3 inputDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 moveVector = Vector3.zero;

        if (inputDir.magnitude >= 0.1f)
        {
            moveVector = inputDir * moveSpeed;

            Quaternion targetRot = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }

        if (cc.isGrounded && verticalVelocity.y < 0f)
            verticalVelocity.y = -1f;
        
        if (attack.WasPressedThisFrame()) // 한 번 실행
        {
            Debug.Log("attack");
        }
        
        if (jump.WasPressedThisFrame()) // 한 번 실행
        {
            verticalVelocity.y = jumpPower;
        }

        if (interaction.IsPressed()) // 여러 번 실행
        {
            Debug.Log("Interact");
        }
        
        verticalVelocity.y += gravity * Time.deltaTime;
        
        Vector3 finalVector = (moveVector + verticalVelocity) * Time.deltaTime;
        cc.Move(finalVector);
    }
}
