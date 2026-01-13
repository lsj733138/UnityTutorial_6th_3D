using UnityEngine;

public class BasicFSM : MonoBehaviour
{
    public enum MonsterState { Idle, Patrol, Trace, Attack }
    public MonsterState monsterState;

    private void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                Debug.Log("Idle");
                break;
            case MonsterState.Patrol:
                Debug.Log("Patrol");
                break;
            case MonsterState.Trace:
                Debug.Log("Trace");
                break;
            case MonsterState.Attack:
                Debug.Log("Attack");
                break;
        }
    }

    public void SetState(MonsterState newState)
    {
        if (monsterState != newState)
            monsterState = newState;
    }
}
