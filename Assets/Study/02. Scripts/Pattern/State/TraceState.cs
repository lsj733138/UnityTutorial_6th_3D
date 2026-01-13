using UnityEngine;

public class TraceState : IState
{
    private CharacterController cc;
    private Animator anim;
    private GameObject prefab;
    private Transform target;

    public TraceState(CharacterController cc, Animator anim, GameObject prefab)
    {
        this.cc = cc;
        this.anim = anim;
        this.prefab = prefab;
    }
    
    public void StateEnter(MonoBehaviour mono)
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        
        Debug.Log("추격 시작");
    }

    public void StateUpdate(MonoBehaviour mono)
    {
        Vector3 moveDir = (target.position - mono.transform.position).normalized;
        cc.Move(moveDir * 5f * Time.deltaTime);
        
        Debug.Log("추격중");
    }

    public void StateExit(MonoBehaviour mono)
    {
        Debug.Log("추격 종료");
    }
}
