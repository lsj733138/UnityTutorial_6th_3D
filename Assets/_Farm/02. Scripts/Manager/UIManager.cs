using UnityEngine;

public class UIManager : SingletonCore<UIManager>
{
    [SerializeField] private GameObject[] popupUIs;
    [SerializeField] private GameObject inventoryUI;
    
    public void InventoryOnOff()
    {
        bool isInventoryActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isInventoryActive);
    }

    public void AllPopUpClose()
    {
        foreach (var UI in popupUIs)
        {
            UI.SetActive(false);
        }
    }
}
