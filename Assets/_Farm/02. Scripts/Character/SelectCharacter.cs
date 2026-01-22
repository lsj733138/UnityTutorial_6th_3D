using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Farm
{
    public class SelectCharacter : MonoBehaviour
    {
        [SerializeField] private Transform centerPivot;

        [SerializeField] private Animator[] characterAnims;

        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button selectButton;

        private int _characterIndex;
        public int CharacterIndex
        {
            get => _characterIndex;
            set
            {
                _characterIndex = value;

                int characterCnt = characterAnims.Length - 1;
          
                if (_characterIndex < 0)
                    _characterIndex = characterCnt;
                else if (_characterIndex > characterCnt)
                    _characterIndex = 0;
            }
        }
        
        private bool isTurn;
        
        private void Start()
        {
            leftButton.onClick.AddListener(TurnLeft);
            rightButton.onClick.AddListener(TurnRight);
            selectButton.onClick.AddListener(Select);
        }

        private void TurnLeft()
        {
            if (isTurn)
                return;
            
            CharacterIndex--;
            
            var targetRot = centerPivot.rotation * Quaternion.Euler(0, -(360f / characterAnims.Length), 0);
            StartCoroutine(TurnRoutine(targetRot));
        }

        private void TurnRight()
        {
            if (isTurn)
                return;
            
            CharacterIndex++;
            
            var targetRot = centerPivot.rotation * Quaternion.Euler(0, 360f / characterAnims.Length, 0);
            StartCoroutine(TurnRoutine(targetRot));
        }

        IEnumerator TurnRoutine(Quaternion targetRot)
        {
            isTurn = true;

            while (true)
            {
                centerPivot.rotation = Quaternion.Slerp(centerPivot.rotation, targetRot, 10f * Time.deltaTime);

                float angle = Quaternion.Angle(centerPivot.rotation, targetRot);
                if (angle <= 0.1f)
                {
                    centerPivot.rotation = targetRot;
                    break;
                }

                yield return null;
            }

            isTurn = false;
        }

        private void Select()
        {
            DataManager.Instance.SelectCharacterIndex = CharacterIndex; // 현재 선택한 캐릭터 인덱스 저장
            DataManager.Instance.SetUnitData("unit" + CharacterIndex);
            StartCoroutine(SelectRoutine());
        }

        IEnumerator SelectRoutine()
        {
            characterAnims[CharacterIndex].SetTrigger("Dance");
            yield return new WaitForSeconds(2f);

            FadeEvent.FadeInvoke(3f, Color.black, true);
            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene(2);
            Debug.Log("씬 전환");
        }
    }
}