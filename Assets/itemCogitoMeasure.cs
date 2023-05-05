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
                sprite.color = new Color(0,1,0,0.5f);
            else
                if (mindist > 5)
                sprite.color = new Color(1,1,0,0.5f);
                else
                  sprite.color = new Color(1,0,0,0.5f);
        }
    }
}
