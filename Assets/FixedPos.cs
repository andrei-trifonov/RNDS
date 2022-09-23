using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FixedPos : MonoBehaviour
{
    [FormerlySerializedAs("point")] [SerializeField] private GameObject Point;
    [SerializeField] private bool noOffset;
    private Vector2 offset;


    // Start is called before the first frame update

    private void Start()
    {
        if (!noOffset)
           offset =  transform.position - Point.transform.position;
    
    }

    public void SetPoint(GameObject point)
    {
        Point = point;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position = (Vector2)Point.transform.position  + offset;
    }
}
