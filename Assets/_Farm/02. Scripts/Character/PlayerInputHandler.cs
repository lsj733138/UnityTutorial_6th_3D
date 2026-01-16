using Farm;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private void OnInventory(InputValue value)
    {
        if (value.isPressed)
        {
            UIManager.Instance.InventoryOnOff();
        }
    }

    private void OnEscape(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log(value.isPressed);
            UIManager.Instance.AllPopUpClose();
        }
    }
}
