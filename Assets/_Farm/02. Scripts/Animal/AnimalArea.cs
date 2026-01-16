using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farm
{
    public class AnimalArea : MonoBehaviour, ITriggerEvent
    {
        public static Action failAction;

        [SerializeField] private TextMeshProUGUI timerUI;
        [SerializeField] private GameObject flag;

        private BoxCollider coll;

        private float timer;
        private bool isInteract;

        private void OnEnable()
        {
            failAction += SetRandomPosition;
        }

        private void OnDisable()
        {
            failAction -= SetRandomPosition;
        }

        private void Awake()
        {
            coll = GetComponent<BoxCollider>();
        }

        public void InteractionEnter()
        {
            isInteract = true;
            timerUI.gameObject.SetActive(true);
            
            CameraManager.OnChangedCamera("Player", "Animal");
            SetRandomPosition();

            StartCoroutine(AnimalRoutine());
        }

        public void InteractionExit()
        {
            isInteract = false;
            timerUI.gameObject.SetActive(false);
            
            CameraManager.OnChangedCamera("Animal", "Player");
            SetFlag(Vector3.zero, false);
            
            Debug.Log($"깃발을 가지고 나오는데 걸린 시간은 {timer:0.##}초 입니다.");
            timer = 0f;
        }

        IEnumerator AnimalRoutine()
        {
            while (isInteract)
            {
                timer += Time.deltaTime;

                int min = Mathf.FloorToInt(timer / 60);
                int sec = Mathf.FloorToInt(timer % 60);
                timerUI.text = $"{min:00} : {sec:00}";
                yield return null;
            }
        }

        private void SetRandomPosition()
        {
            float randX = Random.Range(coll.bounds.min.x, coll.bounds.max.x);
            float randZ = Random.Range(coll.bounds.min.z, coll.bounds.max.z);

            Vector3 randPos = new Vector3(randX, 0f, randZ);
            
            SetFlag(randPos, true);
        }

        private void SetFlag(Vector3 pos, bool isActive)
        {
            flag.transform.SetParent(transform);
            flag.transform.position = pos;
            flag.SetActive(isActive);
        }
    }
}