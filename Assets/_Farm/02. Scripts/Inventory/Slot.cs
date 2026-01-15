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
        dragItem.sprite = null;
        dragItem.gameObject.SetActive(false);
        dragSlot = null;
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
}
