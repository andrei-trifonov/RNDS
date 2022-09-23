using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosDouble : MonoBehaviour
{
    [SerializeField] private GameObject Point1;
    [SerializeField] private GameObject Point2;


    // Start is called before the first frame update



    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position  = new Vector2(Point1.transform.position.x, Point2.transform.position.y) ;
     
    }
}
