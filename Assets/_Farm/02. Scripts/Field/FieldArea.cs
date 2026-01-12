using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Farm
{
    public class FieldArea : MonoBehaviour, ITriggerEvent
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Vector2Int fieldSize = new Vector2Int(10, 10);
        private float tileSize = 2f;
        
        private GameObject[,] tileArray;

        private IField field;
        private FieldSeed seed;
        private FieldHarvest harvest;
        
        [SerializeField] private GameObject[] cropPrefabs;

        [SerializeField] private GameObject fieldUI;
        [SerializeField] private GameObject actionUI; // 어떤 행동을 할지 선택하는 UI
        [SerializeField] private GameObject cropUI; // 심을 작물 선택하는 UI

        [SerializeField] private Button seedButton; // 심기 버튼
        [SerializeField] private Button harvestButton; // 수확하기 버튼
        [SerializeField] private Button[] selectCropButtons; // 심을 작물 선택하는 버튼
        [SerializeField] private Button backButton; // 뒤로가기 버튼
        
        private bool isInteraction;
        private void Start()
        {
            Init();
            
            CreateField();
        }

        private void Init()
        {
            tileArray = new GameObject[fieldSize.x, fieldSize.y];

            seed = new FieldSeed();
            harvest = new FieldHarvest();
            
            // 작물 심는 버튼에 기능 등록
            seedButton.onClick.AddListener(() =>
            {
                field = seed; // 현재 행동 설정
                actionUI.SetActive(false); // 행동 UI 끄기
                cropUI.SetActive(true); // 작물 선택 UI 켜기
            });
            
            harvestButton.onClick.AddListener(() =>
            {
                field = harvest;
            });

            // 작물 버튼마다 그에 맞는 프리팹을 생성하는 기능을 등록
            for (int i = 0; i < selectCropButtons.Length; i++)
            {
                int j = i;
                selectCropButtons[i].onClick.AddListener(() => seed.selectCrop = cropPrefabs[j]);
            }
            
            backButton.onClick.AddListener(() =>
            {
                actionUI.SetActive(true);
                cropUI.SetActive(false);
                seed.selectCrop = null;
            });
        }
        
        IEnumerator FieldRoutine()
        {
            while (isInteraction)
            {
                field?.FieldAction();

                yield return null;
            }
        }
        
        public void InteractionEnter()
        {
            isInteraction = true;
            CameraManager.OnChangedCamera("Player", "Field");
            
            fieldUI.SetActive(true);
            
            StartCoroutine(FieldRoutine());
        }

        public void InteractionExit()
        {
            fieldUI.SetActive(false);
            
            isInteraction = false;
            CameraManager.OnChangedCamera("Field", "Player");
        }
        
        #region 타일생성
        private void CreateField()
        {
            float offsetX = (fieldSize.x - 1) * tileSize / 2f;
            float offsetY = (fieldSize.y - 1) * tileSize / 2f;
            
            for (int i = 0; i < fieldSize.x; i++)
            {
                for (int j = 0; j < fieldSize.y; j++)
                {
                    float posX = transform.position.x + i * tileSize - offsetX;
                    float posZ = transform.position.z + j * tileSize - offsetY;

                    GameObject tileObj = Instantiate(tilePrefab, transform); // transform의 스케일 1

                    tileObj.layer = 15; // Field Layer를 15로 설정

                    tileObj.name = $"Tile_{i}, {j}";
                    tileObj.transform.position = new Vector3(posX, 0, posZ);
                    tileArray[i, j] = tileObj;

                    tileObj.GetComponent<Tile>().arrayPos = new Vector2Int(i, j);
                }
            }
        }
        #endregion
    }
}
