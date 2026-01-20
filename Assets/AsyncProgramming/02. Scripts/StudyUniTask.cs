using Cysharp.Threading.Tasks;
using UnityEngine;

public class Boss
{
    public string BossName;
    public int hp;
    public int dmg;

    public Boss(string bossName, int hp, int dmg)
    {
        this.BossName = bossName;
        this.hp = hp;
        this.dmg = dmg;
    }
}

public class StudyUniTask : MonoBehaviour
{
    // async void Start()
    // {
    //     Debug.Log("Main Thread 시작");
    //     await UniTask.Yield(); // yield return null;
    //
    //     await UniTask.WaitForSeconds(1f); // yield return new WaitForSeconds(1f);
    //     Debug.Log("Main Thread 종료");
    // }

    private Boss boss;
    
    private void Start()
    {
        boss = new Boss("보스", 100, 10);
        
        BackgroundJob().Forget();
        SubMethod();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boss.hp -= 5;
            Debug.Log($"{boss.hp}");
        }
    }

    private void SubMethod()
    {
        Debug.Log("서브 작업 실행");
    }

    private async UniTaskVoid BackgroundJob()
    {
        await UniTask.WaitUntil(() => boss.hp < 75);
        Debug.Log("boss hp < 75"); 
        
        await UniTask.WaitUntil(() => boss.hp < 45);
        Debug.Log("boss hp < 45");
    }
}
