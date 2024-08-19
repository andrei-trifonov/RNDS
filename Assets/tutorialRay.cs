using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorialRay : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform Target;
    public Text Text;
    public string tutorialText;
    private bool Inside;
    public bool unlocksShip;
    private void OnDisable()
    {
        if (unlocksShip)
            GameObject.FindObjectOfType<ShipMove>().TryToUnlock();
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !Target.gameObject.activeSelf)
        {
            lineRenderer.enabled = true;
            Text.text = tutorialText;
            Target.gameObject.SetActive(true);
            Inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && Inside)
        {
            lineRenderer.enabled = false;
            Target.gameObject.SetActive(false);
            Inside = false;
        }
    }

    void FixedUpdate()
    {
        if (Inside)
        {
            // Обновляем позицию линии
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, Target.position);
        }
    }
}