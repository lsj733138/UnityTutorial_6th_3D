using Farm;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private void OnInventory(InputValue value)
    {
        if (value.isPressed)
        {
            GameManager.Instance.UIManager.InventoryOnOff();
        }
    }

    private void OnEscape(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log(value.isPressed);
            GameManager.Instance.UIManager.AllPopUpClose();
        }
    }
}
