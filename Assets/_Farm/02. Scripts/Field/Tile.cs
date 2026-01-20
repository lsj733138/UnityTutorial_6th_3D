using System.Collections;
using UnityEngine;

namespace Farm
{
    public class Tile : MonoBehaviour
    {
        public Vector2Int arrayPos; // 타일 위치
        
        private GameObject cropObj;
        private GameObject fruitPrefab;
        private Crop crop;
        
        private int maxFruitCount;
        private bool isCreate = false; // 작물이 해당 타일에 생성이 되어있는지 판별

        #region 작물 심기
        public void CreateCrop(GameObject cropPrefab)
        {
            if (isCreate)
                return;
            
            cropObj = GameManager.Instance.PoolManager.GetObject(cropPrefab.name);
            
            cropObj.transform.SetParent(transform);
            cropObj.transform.localPosition = Vector3.zero;

            float randY = Random.Range(0, 360);
            Vector3 randRot = new Vector3(0, randY, 0);
            cropObj.transform.localRotation = Quaternion.Euler(randRot);
            
            isCreate = true;
            
            crop = cropObj?.GetComponent<Crop>();
            crop.SetCropData(out fruitPrefab, out maxFruitCount);
        }
        #endregion

        #region 작물 수확하기
        public void HarvestCrop()
        {
            // 타일에 작물이 없을 시 실행 x
            if (!isCreate) 
                return;

            // 작물이 다 자라지 않았을 시에는 실행 x
            if (crop.cropState != Crop.CropState.Level3)
                return;
            
            isCreate = false;

            string cropName = cropObj.name.Replace("(Clone)", "");
            GameManager.Instance.PoolManager.ReleaseObject(cropObj, cropName);
            
            StartCoroutine(HarvestRoutine());
        }

        IEnumerator HarvestRoutine()
        {
            int randAmount = Random.Range(1, maxFruitCount);

            for (int i = 0; i < randAmount; i++) // 열매 생성
            {
                GameObject fruitObj =GameManager.Instance.PoolManager.GetObject(fruitPrefab.name);
                
                fruitObj.transform.position = transform.position + Vector3.up * 0.5f;
                Rigidbody fruitRb = fruitObj.GetComponent<Rigidbody>();

                float randX = Random.Range(-1f, 1f);
                float randZ = Random.Range(-1f, 1f);

                Vector3 forceDir = new Vector3(randX, 3f, randZ);
                fruitRb.AddForce(forceDir, ForceMode.Impulse);

                yield return new WaitForSeconds(0.25f); // 열매들이 겹쳐서 생성되는 걸 막기 위함
            }
        }
        #endregion
    }
}