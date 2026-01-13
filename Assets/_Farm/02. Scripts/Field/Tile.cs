using UnityEngine;

namespace Farm
{
    public class Tile : MonoBehaviour
    {
        public Vector2Int arrayPos;

        private bool isCreate = false;

        public void CreateCrop(GameObject cropPrefab)
        {
            if (isCreate)
                return;

            GameObject cropObj = Instantiate(cropPrefab);
            
            //GameObject cropObj = PoolManager.Instance.pool.Get();
            cropObj.transform.SetParent(transform);
            cropObj.transform.localPosition = Vector3.zero;

            float randY = Random.Range(0, 360);
            Vector3 randRot = new Vector3(0, randY, 0);
            cropObj.transform.localRotation = Quaternion.Euler(randRot);
            
            isCreate = true;
        }
    }
}