using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPrismP : MonoBehaviour
{

    [SerializeField] private GameObject prismComponents;
    [SerializeField] private float Angle;
    [SerializeField] private bool P1;
    [SerializeField] private bool P2;
    GameObject spawned;
    FixedPos fp;
    Raycaster rc;
    private void Start()
    {
        spawned =     Instantiate(prismComponents);
        fp = spawned.GetComponentInChildren<FixedPos>();
        fp.SetPoint(gameObject);
        rc = spawned.GetComponentInChildren<Raycaster>();
        rc.Angle = Angle;
        rc.puzzleItem1 = P1;
        rc.puzzleItem2 = P2;
        rc.setParent(gameObject.transform);
        GetComponent<TakeToGrave>().Objs.Add(spawned);
    }



}
