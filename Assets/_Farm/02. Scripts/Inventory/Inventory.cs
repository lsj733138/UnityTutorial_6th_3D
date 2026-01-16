
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Slot[] slots;

    private void OnEnable()
    {
        QuestManager.Instance.QuestClear += DeleteQuestItem;
    }

    public void GetItem(IItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);

                string questName = item.ItemName.Replace("_Fruit", "");
                QuestManager.Instance.NotifyListener(questName);
                
                return;
            }
        }
    }

    // 퀘스트 완료 시 퀘스트에 필요한 아이템을 인벤토리에서 삭제
    private void DeleteQuestItem(string questName) 
    {
        string itemName = questName + "_Fruit";
        
        foreach (var slot in slots)
        {
            slot.DeleteItem(itemName);
        }
    }
}
