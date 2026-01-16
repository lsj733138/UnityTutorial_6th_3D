using System.Collections;
using Farm;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour, ITriggerEvent
{
    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] private float wanderRadius = 15f;
    
    private float minWaitTime = 1f, maxWaitTime = 5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            SetRandomDestination();
            anim.SetBool("IsWalk", true);

                                              // 길 찾기 종료           남아있는 거리와 정지 거리 비교
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
            
            anim.SetBool("IsWalk", false);
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            
            yield return new WaitForSeconds(1f);
        }
    }

    // 동물의 주변 반경 wanderRadius 안에서 랜덤한 위치를 목적지로 설정 및 이동
    private void SetRandomDestination()
    {
        var randomDir = Random.insideUnitSphere * wanderRadius; // 원을 만들어서 그 안에 특정 지점을 랜덤으로 뽑음

        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
    
    public void InteractionEnter()
    {
        AnimalArea.failAction?.Invoke();
    }

    public void InteractionExit()
    {
    }
}
