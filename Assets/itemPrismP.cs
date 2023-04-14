using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPrismP : MonoBehaviour
{

    [SerializeField] private GameObject prismComponents;
    GameObject spawned;
    FixedPos fp;
    Raycaster rc;
    private void Start()
    {
        spawned =     Instantiate(prismComponents);
        fp = spawned.GetComponentInChildren<FixedPos>();
        fp.SetPoint(gameObject);
        rc = spawned.GetComponentInChildren<Raycaster>();
        rc.setParent(gameObject.transform);
        GetComponent<TakeToGrave>().Objs.Add(spawned);
    }



}
