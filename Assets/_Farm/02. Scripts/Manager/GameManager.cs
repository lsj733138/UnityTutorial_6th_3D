using Farm;
using UnityEngine;

public class GameManager : SingletonCore<GameManager>
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject canvas;

    [field:SerializeField] public PoolManager PoolManager { get; private set; }
    [field:SerializeField] public UIManager UIManager { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        // 캐릭터 생성
        int index = DataManager.Instance.SelectCharacterIndex;
        GameObject character = Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);
        DataManager.Instance.Player = character;
        
        // 풀 생성
        GameObject pool = Instantiate(PoolManager.gameObject);
        PoolManager = pool.GetComponent<PoolManager>();
        pool.transform.SetParent(transform);
        
        // UI Manager 생성
        UIManager = canvas.AddComponent<UIManager>();
    }
    
    private void Start()
    {
        CameraManager.onSetProperty?.Invoke(DataManager.Instance.Player.transform);
    }
}