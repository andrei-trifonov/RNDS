using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosRot : MonoBehaviour
{
    [SerializeField] private GameObject point;
   
    private Vector2 offset;


    // Start is called before the first frame update

    private void Start()
    {
       offset =  transform.position - point.transform.position;
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = (Vector2)point.transform.position;
        transform.rotation = point.transform.rotation;
    }
}
