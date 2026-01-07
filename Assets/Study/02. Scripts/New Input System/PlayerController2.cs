using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    private CharacterController cc;

    private Vector2 moveInput;
    public float moveSpeed = 5f;

    private PlayerInput playerInput;

    private InputAction move;
    private InputAction jump;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        move = playerInput.actions.FindAction("Move");
        jump = playerInput.actions.FindAction("Jump");
    }

    private void OnEnable()
    {
        move.Enable();
        move.performed += Move;
        move.canceled += MoveCancel;
        
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        move.performed -= Move;
        move.canceled -= MoveCancel;
        
        jump.Disable();
        jump.performed -= Jump;
    }

    private void Update()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        cc.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    private void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void MoveCancel(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        
    }
}
