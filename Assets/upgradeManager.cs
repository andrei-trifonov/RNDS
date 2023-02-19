using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> modulesUpgrade;
    public void Upgrade(int i)
    {
        switch (i)
        {
            case 1:
            { 
                modulesUpgrade[1].SetActive(true);
            }
                break;
        
        }
    }
}
