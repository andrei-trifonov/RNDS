using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsHolds : MonoBehaviour
{
    private int nowHolds = -1;
    private GameObject nowHoldsObj;
    
    public void AddItem(int objNum, GameObject obj)
    {
        nowHolds = objNum;
        nowHoldsObj = obj;
    }
    public int ItemNum()
    {
        return nowHolds;
    }
    public GameObject Item()
    {
        return nowHoldsObj;
    }


}
