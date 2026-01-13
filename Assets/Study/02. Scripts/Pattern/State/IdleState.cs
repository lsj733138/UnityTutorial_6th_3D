using UnityEngine;

public class IdleState : IState
{
    public void StateEnter(MonoBehaviour mono)
    {
        Debug.Log("대기 시작");
    }

    public void StateUpdate(MonoBehaviour mono)
    {
        Debug.Log("대기중");
    }

    public void StateExit(MonoBehaviour mono)
    {
        Debug.Log("대기 종료");
    }
}
