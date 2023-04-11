using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask lMask;
    private void Update()
    {
        //Ray ray = new Ray(origin,direction);
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, lMask ))
        {
            lineRenderer.enabled = true;
            var firstPosition = transform.position ;
            var secondPosition = hit.point;
            lineRenderer.SetPosition(0, firstPosition);
            lineRenderer.SetPosition(1, secondPosition);
        }
        else
        {
            lineRenderer.enabled = false;
         }
    }
}
