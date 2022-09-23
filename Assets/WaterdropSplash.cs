using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WaterdropSplash : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private GameObject Waterdrops;

    private float yPos;
    // Start is called before the first frame update


    void FixedUpdate()
    {
        var transform1 = transform.position;
        gameObject.transform.position = new Vector3(Waterdrops.transform.position.x, transform1.y, transform1.z);
    }

}
