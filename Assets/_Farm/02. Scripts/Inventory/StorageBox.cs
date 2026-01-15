using System.Collections;
using UnityEngine;

public class StorageBox : MonoBehaviour,ITriggerEvent
{
    private Animator anim;

    [SerializeField] private GameObject storageUI;
    [SerializeField] private GameObject inventoryUI;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InteractionEnter()
    {
        anim.SetTrigger("Open");

        StartCoroutine(OpenRoutine());
    }

    IEnumerator OpenRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        
        inventoryUI.SetActive(true);
        storageUI.SetActive(true);
    }

    public void InteractionExit()
    {
        anim.SetTrigger("Close");
        
        inventoryUI.SetActive(false);
        storageUI.SetActive(false);
    }
}
