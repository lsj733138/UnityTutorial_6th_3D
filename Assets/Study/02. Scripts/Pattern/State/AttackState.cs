using UnityEngine;


public class AttackState : IState
{
    public void StateEnter(MonoBehaviour mono)
    {
        Debug.Log("공격 시작");
    }

    public void StateUpdate(MonoBehaviour mono)
    {
        Debug.Log("공격중");
    }

    public void StateExit(MonoBehaviour mono)
    {
        Debug.Log("공격 종료");
    }
}

