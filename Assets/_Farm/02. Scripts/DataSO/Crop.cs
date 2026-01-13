using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public enum CropState { Level1, Level2, Level3 }
    public CropState cropState;
    
    [SerializeField] private CropData data;

    private float growthTime;
    public bool isHarvest;

    private void Awake()
    {
        growthTime = data.growthTime;
    }

    private void OnEnable()
    {
        isHarvest = false;

        SetState(CropState.Level1);
        
        StartCoroutine(GrowthRoutine());
    }

    IEnumerator GrowthRoutine()
    {
        yield return new WaitForSeconds(growthTime);
        
        //Level2로 변경
        SetState(CropState.Level2);
        
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(growthTime);
        
        //Level3로 변경
        SetState(CropState.Level3);
        
        isHarvest = true;
    }

    private void SetState(CropState newState)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == (int)newState);
        }
    }
}
