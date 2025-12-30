using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    //public Transform[] points;
    public NavMeshSurface surface;

    public float bakedDistance = 10f;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        surface.transform.position = transform.position;
        surface.BuildNavMesh(); // Bake 기능
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        
        agent.SetDestination(transform.position + dir);

        float dist = Vector3.Distance(transform.position, surface.transform.position);
        if (dist >= bakedDistance)
        {
            surface.transform.position = transform.position;
            surface.BuildNavMesh(); // Bake 기능
        }
    }
    
    
}
