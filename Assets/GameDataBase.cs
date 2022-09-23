using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataBase : MonoBehaviour
{
    [SerializeField] private GameObject[] hookItemList;
    // Start is called before the first frame update

    public GameObject GetItemFromList(int index)
    {
        return hookItemList[index];
    }


}
