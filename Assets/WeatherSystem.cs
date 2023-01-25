using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;
[System.Serializable]
public struct ListOfWeather
{
    public List<GameObject> Weather;
}
public class WeatherSystem : MonoBehaviour
{
    [SerializeField] private List<ListOfWeather> Weather;
    [SerializeField] private Vector2 timeBounds;
    private void Start()
    {
        foreach (ListOfWeather obj in Weather)
        {
            foreach (var obj2 in obj.Weather)
            {
                obj2.SetActive(false);
            }
            
        }

        StartCoroutine(WeatherWaitCoroutine());

    }
    public void SetTimeBounds(float first, float second)
    {
        timeBounds.x = first;
        timeBounds.y = second;
    }
    IEnumerator WeatherWaitCoroutine()
    {
        
        yield return new WaitForSeconds(Random.Range(timeBounds.x, timeBounds.y));
        StartCoroutine(WeatherCoroutine(Random.Range(0, (Weather.Count-1))));
    }
    IEnumerator WeatherCoroutine(int weatherNum)
    {

        foreach (var obj in Weather[weatherNum].Weather)
        {
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(Random.Range(timeBounds.x, timeBounds.y));
        foreach (var obj in Weather[weatherNum].Weather)
        {
            obj.SetActive(false);
        }
        StartCoroutine(WeatherWaitCoroutine());
    }
}
