using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] float Angle;
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform Parent;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask lMask;
    [SerializeField] private Collider2D myCollider;
    private int pointNum;
   
    public void setParent(Transform par)
    {
        Parent = par;
    }
    private void FixedUpdate()
    {
        Vector2 ray;
        //Ray ray = new Ray(origin,direction);
        if (Parent.lossyScale.x>0)
           ray = new Vector2(Parent.right.x + Mathf.Sin(Angle) * 100, Parent.right.y + Mathf.Cos(Angle) * 100);
        else
            ray = new Vector2 (-Parent.right.x - Mathf.Sin(Angle) * 100, Parent.right.y + Mathf.Cos(Angle) * 100);
      
        lineRenderer.SetPosition(0, new Vector3 ( Parent.position.x, Parent.position.y, 1));
        lineRenderer.SetPosition(1, ray*maxDistance);

        RaycastHit2D hit = (Physics2D.Raycast(Parent.position, ray, maxDistance, lMask));
        if (hit.collider!=null && hit.collider != myCollider)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Prism"))
            {
                //Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<itemPrism>().Cast();
            }
            lineRenderer.enabled = true;
            Vector2 firstPosition = Parent.position;
            Vector2 secondPosition = hit.point;
            lineRenderer.SetPosition(0, firstPosition);
            lineRenderer.SetPosition(1, secondPosition);
        }
  
    }
}
