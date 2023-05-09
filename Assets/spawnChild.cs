using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnChild : MonoBehaviour
{
    [SerializeField] private GameObject Child;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawned  = Instantiate(Child);
        spawned.GetComponent<FixedPos>().SetPoint(gameObject);
    }
    
}
