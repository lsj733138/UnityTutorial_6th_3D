using UnityEngine;

public interface IState
{
    void StateEnter(MonoBehaviour mono); // 상태 시작
    void StateUpdate(MonoBehaviour mono); // 상태 진행중
    void StateExit(MonoBehaviour mono); // 상태 종료
}
