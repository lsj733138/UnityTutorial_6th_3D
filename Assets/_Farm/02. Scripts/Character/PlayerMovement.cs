using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private Animator anim;
    
    private Vector3 moveInput; // 입력 되는 값
    private Vector3 moveVector; // 움직일 방향
    private Vector3 verticalVelocity;

    [SerializeField] private GameObject inventoryUI;
    
    private float currSpeed;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    
    //[SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotSpeed = 10f;

    [SerializeField] private float gravity = -30f;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Turn();
        SetAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<ITriggerEvent>();
        
        if (interactable != null)
            interactable.InteractionEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<ITriggerEvent>();
        
        if (interactable != null)
            interactable.InteractionExit();
    }

    private void Move()
    {
        if (moveInput.magnitude >= 0.1f) // 움직일 때
        {
            bool isRun = Input.GetKey(KeyCode.LeftShift);
            currSpeed = isRun ? runSpeed : walkSpeed;
            
            if (cc.isGrounded && verticalVelocity.y < 0) // 바닥에 닿은 상태일 때, 중력 초기화
                verticalVelocity.y = -1f;

            verticalVelocity.y += gravity * Time.deltaTime;
            
            moveVector = moveInput * currSpeed;
           
            Vector3 finalMovement = (moveVector + verticalVelocity) * Time.deltaTime;
            cc.Move(finalMovement);
        }
        else // 안 움직일 때
        {
            currSpeed = 0f;
            cc.Move(verticalVelocity * Time.deltaTime);
        }
        
    }

    private void Turn()
    {
        if (moveInput.magnitude >= 0.1f) // 회전
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }

    private void SetAnimation()
    {
        // 아무 키도 누르지 않으면 속도 0 -> Idle
        // WASD를 누르면 속도 3 -> Walk
        // WASD + Shift를 누르면 속도 6 -> Run
        
        anim.SetFloat("Speed", currSpeed, 0.1f, Time.deltaTime);
    }
    
    private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveInput = new Vector3(inputDir.x, 0f, inputDir.y);
    }

    private void OnInventory(InputValue value)
    {
        if (value.isPressed)
        {
            bool isActive = inventoryUI.activeSelf;
            
            inventoryUI.SetActive(!isActive);
        }
    }
}
