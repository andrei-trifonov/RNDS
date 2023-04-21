using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserComponents;
    [SerializeField] private float Angle;
    Raycaster rc;
    GameObject spawned;
    // Start is called before the first frame update
    void Start()
    {
        spawned = Instantiate(laserComponents);

        rc = spawned.GetComponentInChildren<Raycaster>();
        rc.setParent(gameObject.transform);
        rc.Angle = Angle;
        GetComponent<TakeToGrave>().Objs.Add(spawned);
    }


}
