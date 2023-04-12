using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform Parent;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask lMask;

    private int pointNum;
   

    private void FixedUpdate()
    {
        Vector2 ray;
        //Ray ray = new Ray(origin,direction);
        if (Parent.lossyScale.x>0)
           ray =  Parent.right;
        else
            ray = -Parent.right;
      
        lineRenderer.SetPosition(0, Parent.position);
        lineRenderer.SetPosition(1, ray*maxDistance);

        RaycastHit2D hit = (Physics2D.Raycast(Parent.position, ray, maxDistance, lMask));
        if (hit.collider!=null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Prism"))
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.rigidbody.gameObject.GetComponentInChildren<itemPrism>().Cast(ray- (Vector2)Parent.position);
            }
            lineRenderer.enabled = true;
            Vector2 firstPosition = Parent.position;
            Vector2 secondPosition = hit.point;
            lineRenderer.SetPosition(0, firstPosition);
            lineRenderer.SetPosition(1, secondPosition);
        }
  
    }
}
