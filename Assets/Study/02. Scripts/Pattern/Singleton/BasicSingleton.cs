using UnityEngine;


public class BasicSingleton : MonoBehaviour
{
    private static BasicSingleton instance;
    public static BasicSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindFirstObjectByType<BasicSingleton>(); // 씬 상에 오브젝트가 존재하는지 찾음

                if (obj != null) // 씬 상에 오브젝트는 있을 시 instance에 할당
                {
                    instance = obj;
                }
                else // 씬 상에 존재조차 하지 않을 경우 직접 새로 만들어서 할당
                {
                    var newObj = new GameObject("Basic Singleton");
                    instance = newObj.AddComponent<BasicSingleton>();
                }

            }
            
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // 자신(오브젝트)를 instance에 할당
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }
}
