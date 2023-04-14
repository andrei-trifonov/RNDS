using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeToGrave : MonoBehaviour
{
    public List<GameObject> Objs;
    private void OnDestroy()
    {
        foreach(GameObject obj in Objs)
        {
            GameObject.Destroy(obj);
        }
    }
    private void OnDisable()
    {
        foreach (GameObject obj in Objs)
        {
            GameObject.Destroy(obj);
        }
    }
}
