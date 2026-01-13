using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Pool;

namespace Farm
{
    public class PoolManager : SingletonCore<PoolManager> // 여러 곳에서 접근하기에 싱글톤으로
    {
        public ObjectPool<GameObject> pool;
        public GameObject prefab;

        protected override void Awake()
        {
            base.Awake();
            //pool = new ObjectPool<GameObject>(createObject, GetObeject, ReleaseObject);
            pool = new ObjectPool<GameObject>(createObject, 
                (obj) => obj.SetActive(true), 
                (obj) => obj.SetActive(false));
        }

        private GameObject createObject()
        {
            GameObject obj = Instantiate(prefab);

            return obj;
        }

        private void GetObeject(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void ReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}