using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Farm
{
    public class PoolManager :MonoBehaviour // 여러 곳에서 접근하기에 싱글톤으로
    {
        [Serializable]
        public class PoolData
        {
            public string name;
            public GameObject prefab;
        }

        public List<PoolData> poolList = new List<PoolData>();
        // 이름으로 풀 검색 기능
        private Dictionary<string, IObjectPool<GameObject>> poolDics = new Dictionary<string, IObjectPool<GameObject>>();

        public GameObject prefab;
        
       private void Awake()
        {
          

            // Pool을 생성하고 Dictionary에 등록하는 기능
            foreach (var poolData in poolList)
            {
                poolDics[poolData.name] = new ObjectPool<GameObject>(
                    createFunc: () => Instantiate(poolData.prefab),
                    actionOnGet: (obj) => obj.SetActive(true),
                    actionOnRelease:(obj) => obj.SetActive(false));
                Debug.Log(poolData.name);
            }
        }

        public GameObject GetObject(string key)
        {
            if (poolDics.ContainsKey(key))
            {
                GameObject obj = poolDics[key].Get();

                return obj;
            }

            Debug.LogError($"{key} is not found in Pool");
            return null;
        }

        public void ReleaseObject(GameObject obj, string key)
        {
            if (poolDics.ContainsKey(key))
            {
                poolDics[key].Release(obj);
            }
            else
                Debug.LogError($"{key} is not found in Pool");
        }
    }
}