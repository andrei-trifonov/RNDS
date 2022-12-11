using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class Parallax : MonoBehaviour
{
    
    [SerializeField] private Transform cameraLastPos;
    [SerializeField] private List<GameObject> Layers;
    [FormerlySerializedAs("Modifier")] [SerializeField] private List<float> ModifierX;

    [SerializeField] private float ModifierY;
    [SerializeField] private float ymaxOffset;
    private float wayPercentage;
     private Vector3[] startPositions = new Vector3 [10];
     private float yOffset, ytranslitedOffset ;
    private void Start()
    {
        for (int i = 0; i < Layers.Count; i++)
        {
            startPositions[i] = Layers[i].transform.localPosition;
        }
    
    }

    
    private void FixedUpdate()
    {
        var camPos = gameObject.transform.position;
        wayPercentage = (gameObject.transform.position.x / cameraLastPos.transform.position.x );
        for (int i = 0; i < Layers.Count; i++)
        {
           
            Layers[i].transform.localPosition = new Vector3( ( camPos.x  -wayPercentage * ModifierX[i]), startPositions[i].y - ymaxOffset * camPos.y - startPositions[i].y / ModifierY , startPositions[i].z);
        }
    }

}