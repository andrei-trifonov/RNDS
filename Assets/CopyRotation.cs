using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    private void FixedUpdate()
    {
        gameObject.transform.rotation = Target.transform.rotation;
    }
}
