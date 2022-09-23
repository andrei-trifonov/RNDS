using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTranslator : MonoBehaviour
{
    [SerializeField] private MagneticItem MI;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Moving"))
        {
            MI.PlaySound();
        }

    }
}
