using System.Threading;
using UnityEngine;

public class StudyThread : MonoBehaviour
{

    private void Start()
    {
        Thread subThread = new Thread(MethodA);
    
        subThread.IsBackground = true; // 유니티 종료시 같이 종료
        
        subThread.Start();
    
        subThread.Join(); // Thread가 완료될 때까지 대기 -> 동기 방식
        
        Debug.Log("Main Thread 종료");
    }
    
    private void MethodA()
    {
        Debug.Log("Sub Thread 실행");
        Thread.Sleep(2000); // 2초 멈춤
        
        Debug.Log("Sub Thread 완료");
    }


}
