using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TintLightSwitch : MonoBehaviour
{
    private int i;
    public List<Transform> transform_list;
    private Transform last;
    private void Start()
    {
        Transform[] transform_list1 = GetComponentsInChildren<Transform>();
        transform_list = transform_list1.ToList();
        transform_list.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (i<transform_list.Count)
            i++;
        else
        {
            i = 0;
        }
        if (last && last!=gameObject.transform)
            last.gameObject.SetActive(false);
        last = transform_list[i];
        last.gameObject.SetActive(true);


    }
}
