using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserComponents;
    Raycaster rc;
    GameObject spawned;
    // Start is called before the first frame update
    void Start()
    {
        spawned = Instantiate(laserComponents);

        rc = spawned.GetComponentInChildren<Raycaster>();
        rc.setParent(gameObject.transform);
        GetComponent<TakeToGrave>().Objs.Add(spawned);
    }


}
