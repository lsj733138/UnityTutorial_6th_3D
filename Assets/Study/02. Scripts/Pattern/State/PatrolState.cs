using UnityEngine;


public class PatrolState : IState
{
    public void StateEnter(MonoBehaviour mono)
    {
        Debug.Log("정찰 시작");
    }

    public void StateUpdate(MonoBehaviour mono)
    {
        Debug.Log("정찰중");
    }

    public void StateExit(MonoBehaviour mono)
    {
        Debug.Log("정찰 종료");
    }
}  