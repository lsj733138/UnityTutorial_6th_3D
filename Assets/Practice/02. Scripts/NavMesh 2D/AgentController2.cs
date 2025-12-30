using UnityEngine;
using UnityEngine.AI;

public class AgentController2 : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform destination;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(destination.position);
    }
}
