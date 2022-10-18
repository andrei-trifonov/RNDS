using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outerObstacle : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
