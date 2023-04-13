using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPrism : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask lMask;
    public float deflectionAngle;
    private bool casting;
    public void Cast(Vector2 normal)
    {

        // Calculate the direction of the deflected laser
        casting = true;
        Vector2 deflectedDirection = new Vector2(normal.y, normal.x);
        RaycastHit2D hit = (Physics2D.Raycast(transform.position, deflectedDirection, maxDistance, lMask));
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, deflectedDirection*2);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Prism"))
            {
             //   Debug.Log(hit.collider.gameObject.name);
                hit.rigidbody.gameObject.GetComponentInChildren<itemPrism>().Cast(hit.normal);
            }
            lineRenderer.enabled = true;
            var firstPosition = transform.position;
            var secondPosition = hit.point;
            lineRenderer.SetPosition(0, firstPosition);
            lineRenderer.SetPosition(1, secondPosition);
        }
    }
    private void FixedUpdate()
    {
            lineRenderer.enabled = casting;
            casting = false;
        

        
    }

}
