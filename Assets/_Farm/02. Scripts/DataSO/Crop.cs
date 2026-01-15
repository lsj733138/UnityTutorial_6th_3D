using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public enum CropState { Level1, Level2, Level3 }
    public CropState cropState;
    
    [SerializeField] private CropData data;

    private float growthTime;

    private WeatherType currentWeather = WeatherType.Sun;
    private float originGrowthTime; // 기본 성장 시간
    
    private void OnEnable()
    {
        growthTime = data.growthTime;
        originGrowthTime = data.growthTime;
        
        SetState(CropState.Level1);
        WeatherSystem.weatherChanged += SetGrowth;
        
        StartCoroutine(GrowthRoutine());
    }

    private void OnDisable()
    {
        WeatherSystem.weatherChanged -= SetGrowth;
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
    }

    private void SetState(CropState newState)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == (int)newState);

        cropState = newState;
    }
    
    // 날씨 변화에 따른 작물의 성장시간 조절
    private void SetGrowth(WeatherType weatherType)
    {
        if (currentWeather == weatherType)
            return;
        
        switch (weatherType)
        {
            case WeatherType.Sun:
                growthTime = originGrowthTime; // 기본성장시간으로 되돌린 후 시간 추가
                growthTime *= 1f;
                break;
            case WeatherType.Rain:
                growthTime = originGrowthTime;
                growthTime *= 1.3f;
                break;
            case WeatherType.Snow:
                growthTime = originGrowthTime;
                growthTime *= 2f;
                break;
        }

        currentWeather = weatherType;
        //Debug.Log("성장 속도 변경");
    }

    public void SetCropData(out GameObject fruit, out int maxCount)
    {
        fruit = data.fruit;
        maxCount = data.maxFruitCount;
    }
}
