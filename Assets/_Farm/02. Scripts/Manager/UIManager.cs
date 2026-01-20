using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> popupUIs = new List<GameObject>();
    [SerializeField] private GameObject inventoryUI;

    private void Start()
    {
        popupUIs.Add(transform.Find("Popup/Inventory").gameObject);
        popupUIs.Add(transform.Find("Popup/Storage Box").gameObject);
        popupUIs.Add(transform.Find("Popup/Quest").gameObject);

        inventoryUI = popupUIs[0];
    }

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
