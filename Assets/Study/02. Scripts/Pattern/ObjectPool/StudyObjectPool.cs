using System.Collections.Generic;
using UnityEngine;

public class StudyObjectPool : MonoBehaviour
{
    public Queue<GameObject> objQueue = new Queue<GameObject>();

    public GameObject objPrefab;
    public Transform parent;

    private void Start()
    {
        
    }

    private void CreateObject(int amount) // 오브젝트를 만들어 Queue에 넣음
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(objPrefab, parent);
            EnqueueObject(obj);
        }
    }

    public void EnqueueObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        obj.SetActive(false);
        
        objQueue.Enqueue(obj);
    }

    public GameObject DequeueObject()
    {
        if (objQueue.Count < 3)
            CreateObject(50);
        
        GameObject obj = objQueue.Dequeue();

        obj.SetActive(true);

        return obj;
    }
}
