using UnityEngine;

namespace Farm
{
    public class DataManager : SingletonCore<DataManager>
    {
        private int _selectCharacterIndex;
        public int SelectCharacterIndex
        {
            get => _selectCharacterIndex;
            set
            {
                Debug.Log($"선택한 캐릭터는 {value}번째 입니다.");
                _selectCharacterIndex = value;
            }
        }
        
        public GameObject Player { get; set; }
        public string UserID { get; set; }
    }
}