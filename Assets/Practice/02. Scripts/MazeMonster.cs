using System;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class MazeMonster : MonoBehaviour
    {
        private NavMeshAgent agent;

        public Transform player;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            agent.SetDestination(player.position);
        }
    }
}