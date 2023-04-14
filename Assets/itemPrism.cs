using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPrism : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
 
 
    private bool casting;
    public void Cast()
    {

        // Calculate the direction of the deflected laser
        casting = true;
       
    }
    private void FixedUpdate()
    {
            lineRenderer.enabled = casting;
            casting = false;
    }

}
