using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StudyUniTaskLock : MonoBehaviour
{
    private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    async void Start()
    {
        Debug.Log("테스트 시작");

        UniTask t1 = UniTask.Run(() => SubMethod("T1"));
        UniTask t2 = UniTask.Run(() => SubMethod("T2"));
    }

    private async UniTaskVoid SubMethod(string msg)
    {
        await semaphore.WaitAsync(); // 열쇠가 없으면 대기

        try
        {
            Debug.Log($"{msg} 쓰레드 실행");
            await UniTask.Delay(500);
            
            Debug.Log($"{msg} 쓰레드 실행중");
            await UniTask.Delay(500);
            
            Debug.Log($"{msg} 쓰레드 종료");
        }
        finally
        {
            semaphore.Release();
        }
    }
}
