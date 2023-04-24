using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] public bool puzzleItem1;
    [SerializeField] public bool puzzleItem2;
    public float Angle;
    [SerializeField] private float maxDistance;
    [SerializeField] private float buffer;
    [SerializeField] private Transform Parent;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask lMask;
    //[SerializeField] private Collider2D myCollider;
    [SerializeField] private bool  Prism;
    public bool casting;
    public void setParent(Transform par)
    {
        Parent = par;
        if (!Prism)
            lineRenderer.enabled = false;
    }
    private void FixedUpdate()
    {
        Vector2 ray;
        //Ray ray = new Ray(origin,direction);
        if (Parent.lossyScale.x>0)
           ray = new Vector2(Vector2.right.x + Mathf.Sin(Mathf.Deg2Rad*Angle) * 100, Parent.right.y + Mathf.Cos(Mathf.Deg2Rad * Angle) * 100);
        else
            ray = new Vector2 (-Vector2.right.x - Mathf.Sin(Mathf.Deg2Rad * Angle) * 100, Parent.right.y + Mathf.Cos(Mathf.Deg2Rad * Angle) * 100);
      
        lineRenderer.SetPosition(0, new Vector3 ( Parent.position.x, Parent.position.y, 1));
        lineRenderer.SetPosition(1, (Vector2)Parent.position + ray * 0.12f);

        RaycastHit2D hit = (Physics2D.Raycast((Vector2)Parent.position + ray *buffer , ray, maxDistance, lMask));
        Debug.DrawRay((Vector2)Parent.position + ray * buffer, ray);
        if (hit.collider!=null)
        {
            try
            {
                if (casting || !Prism)
                {

                    hit.collider.GetComponent<Raycaster>().casting = true;
                }
                lineRenderer.SetPosition(1, hit.point);
               
            }
            catch
            {

            }
            try {
                if (puzzleItem1 && casting)
                {
                    hit.collider.GetComponent<puzzleFinger>().Laser1 = true;
                }
                if (puzzleItem2 && casting)
                {
                    hit.collider.GetComponent<puzzleFinger>().Laser2 = true;
                }
            } catch { }
            Vector2 firstPosition = Parent.position;
            Vector2 secondPosition = hit.point;
            lineRenderer.SetPosition(0, firstPosition);
            lineRenderer.SetPosition(1, secondPosition);
        }
        if (casting)
        {
            lineRenderer.enabled = true;
            casting = false;
        }
        else
        {
            lineRenderer.enabled = false;
        }
        if (!Prism)
        {
            lineRenderer.enabled = true;
        }
    }
}
