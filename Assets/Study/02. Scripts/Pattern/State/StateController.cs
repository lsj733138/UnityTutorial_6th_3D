using System;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private IState currentState;
    private IState idle, patrol, trace, attack;

    private CharacterController cc;
    private Animator anim;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        idle = new IdleState();
        patrol = new PatrolState();
        trace = new TraceState(cc, anim, prefab);
        attack = new AttackState();
        
        currentState = new IdleState();
    }

    private void Update()
    {
        if (currentState != null)
            currentState.StateUpdate(this);

        if (Input.GetKeyDown(KeyCode.Q))
            SetState(idle);
        else if (Input.GetKeyDown(KeyCode.W))
            SetState(patrol);
        else if (Input.GetKeyDown(KeyCode.E))
            SetState(trace);
        else if (Input.GetKeyDown(KeyCode.R))
            SetState(attack);
    }

    public void SetState(IState newState)
    {
        if (currentState != newState)
        {
            currentState?.StateExit(this); // 기존 상태의 exit
            
            currentState = newState; // 상태 변경
            
            currentState?.StateEnter(this); // 새로운 상태의 enter
        }
    }
}
