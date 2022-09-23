using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDB : MonoBehaviour
{
    [SerializeField] private int itemID;

    public int GetItemID()
    {
        return itemID;
    }

}
