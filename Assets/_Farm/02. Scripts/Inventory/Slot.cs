using Farm;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private IItem item;

    [SerializeField] private Button slotButton;
    [SerializeField] private Image slotImage;

    [SerializeField] private Image dragItem;
    private static Slot dragSlot;
    
    public bool IsEmpty { get; private set; } = true;

    private void Awake()
    {
        slotButton.onClick.AddListener(UseItem);
    }

    private void OnEnable()
    {
        slotImage.gameObject.SetActive(!IsEmpty); // 비어있지 않으면 켜기
        slotButton.interactable = !IsEmpty; 
    }

    public void AddItem(IItem item)
    {
        IsEmpty = false;
        this.item = item;
        slotImage.sprite = item.Icon;
        
        slotImage.gameObject.SetActive(true);
        slotButton.interactable = true;
    }
    
    private void UseItem()
    {
        if (item == null)
            return;

        item.Use();
        item = null;
        IsEmpty = true;
        slotImage.gameObject.SetActive(false);
        slotImage.sprite = null;
        slotButton.interactable = false;
    }

    public void DeleteItem(string itemName)
    {
        if (item == null)
            return;
        
        if (item.ItemName == itemName)
        {
            item = null;
            IsEmpty = true;
            slotImage.gameObject.SetActive(false);
            slotImage.sprite = null;
            slotButton.interactable = false;
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEmpty)
            return;

        dragSlot = this;
        dragItem.sprite = item.Icon;
        dragItem.gameObject.SetActive(true);
        dragItem.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragItem.transform.position = eventData.position; // 현재 마우스 위치로 이미지 이동
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 아이템 정보 swap
        IItem tmpItem = this.item;
        SetItem(dragSlot.item);
        dragSlot.SetItem(tmpItem);
        
        Debug.Log("아이템 이동 완료");

        dragItem.sprite = null;
        dragItem.gameObject.SetActive(false);
        dragSlot = null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // 놓은 장소가 UI가 아닐 시
            DropItemToWorld();
        
        dragItem.sprite = null;
        dragItem.gameObject.SetActive(false);
        dragSlot = null;

        dragItem.raycastTarget = true;
    }

    public void SetItem(IItem newItem)
    {
        this.item = newItem;
        if (newItem == null)
        {
            IsEmpty = true;
            slotImage.sprite = null;
            slotImage.gameObject.SetActive(false);
            slotButton.interactable = false;
        }
        else
        {
            IsEmpty = false;
            slotImage.sprite = newItem.Icon;
            slotImage.gameObject.SetActive(true);
            slotButton.interactable = true;
        }
    }

    private void DropItemToWorld()
    {
        if (item == null)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePos); // 마우스 위치를 월드 좌표로 전환
        GameObject dropItem = GameManager.Instance.PoolManager.GetObject(item.ItemName);
        Debug.Log($"{item.ItemName}을 바닥에 버렸습니다.");
        dropItem.transform.position = spawnPos + Vector3.up * 4f;
        
        SetItem(null);
      
        
    }
}
