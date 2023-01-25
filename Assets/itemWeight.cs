using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemWeight : MonoBehaviour
{
    [SerializeField] private int Weight;

    public int GetWeight()
    {
        return Weight;
    }
}
