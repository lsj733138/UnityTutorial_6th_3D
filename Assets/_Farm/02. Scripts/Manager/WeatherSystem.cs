using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeatherType
{
    Sun,
    Rain,
    Snow
}

public class WeatherSystem : MonoBehaviour
{
    public WeatherType weatherType;

    [SerializeField] private GameObject[] weatherParticles;

    public static event Action<WeatherType> weatherChanged; 
    
    IEnumerator Start()
    {
        while (true)
        {
            int weatherCount = weatherParticles.Length;
            int randIndex = Random.Range(0, weatherCount);

            for (int i = 0; i < weatherCount; i++)
            {
                weatherParticles[i].SetActive(randIndex == i);
            }

            weatherType = (WeatherType)randIndex;
            weatherChanged?.Invoke(weatherType);
            
            Debug.Log($"현재 날씨는 {weatherType}입니다");

            yield return new WaitForSeconds(10f);
        }
    }
}
