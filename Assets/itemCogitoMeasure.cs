using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCogitoMeasure : MonoBehaviour
{
    private GameObject[] cogitoSources;
    [SerializeField] private SpriteRenderer sprite;
    private float mindist = 999;
    private void Start()
    {
        cogitoSources = GameObject.FindGameObjectsWithTag("Cogito");
    }
    private void FixedUpdate()
    {
        mindist = 999;
        foreach (GameObject source in cogitoSources)
        {
            
            float dist = Vector2.Distance(source.transform.position, transform.position);
            if (mindist > dist)
                mindist = dist;
            if (mindist > 10)
                sprite.color = Color.green;
            else
                if (mindist > 5)
                sprite.color = Color.yellow;
                else
                  sprite.color = Color.red;
        }
    }
}
