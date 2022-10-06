using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> Weather;
    [SerializeField] private Vector2 timeBounds;
    private void Start()
    {
        foreach (var obj in Weather)
        {
            obj.SetActive(false);
        }

        StartCoroutine(WeatherWaitCoroutine());

    }
    IEnumerator WeatherWaitCoroutine()
    {
        
        yield return new WaitForSeconds(Random.Range(timeBounds.x, timeBounds.y));
        StartCoroutine(WeatherCoroutine(Random.Range(0, (Weather.Count-1))));
    }
    IEnumerator WeatherCoroutine(int weatherNum)
    {
        Weather[weatherNum].SetActive(true);
        yield return new WaitForSeconds(Random.Range(timeBounds.x, timeBounds.y));
        Weather[weatherNum].SetActive(false);
        StartCoroutine(WeatherWaitCoroutine());
    }
}
