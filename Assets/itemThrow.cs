using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemThrow : MonoBehaviour
{

    public void Throw() {

        try
        {
            GetComponentInParent<CarryManager>().ThrowItem();
            GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-transform.parent.lossyScale.x*250, 400));
        }
        catch { }
    }
}
