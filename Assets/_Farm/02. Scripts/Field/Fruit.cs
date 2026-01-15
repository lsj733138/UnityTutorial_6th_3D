using UnityEngine;

namespace Farm
{
    public class Fruit : MonoBehaviour, ITriggerEvent, IItem
    {
        public Inventory Inven { get; private set; }
        public GameObject Obj { get; private set; }
        public string ItemName { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }

        private void Awake()
        {
            Inven = FindFirstObjectByType<Inventory>();
            Obj = gameObject;

            ItemName = gameObject.name.Replace("(Clone)", "");
        }

        public void InteractionEnter()
        {
            Get();
        }
        public void InteractionExit() {}

        public void Get()
        {
            PoolManager.Instance.ReleaseObject(gameObject, ItemName);
            
            Debug.Log($"{ItemName} 획득");
            Inven.GetItem(this);
        }

        public void Use()
        {
            Debug.Log($"{ItemName} 사용");
        }
    }
}