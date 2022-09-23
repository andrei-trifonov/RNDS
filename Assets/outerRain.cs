using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class outerRain : MonoBehaviour
{
    private GameObject hookedItem;
    private CarryManager o_CManager;
    private itemBucket o_itemBucket;
    private bool Blocked;
    private bool Inside;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            o_CManager = other.GetComponent<CarryManager>();
            Inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inside = false;
        }
    }
    private void FixedUpdate()
    {
        if (!Blocked && Inside)
        {
            StartCoroutine(AddWaterCoroutine());
        }
    }

    IEnumerator AddWaterCoroutine()
    {
        if (o_CManager.GetPickedItem())
        {
            hookedItem = o_CManager.GetPickedItem().transform.GetChild(0).gameObject;
            if ((o_itemBucket = hookedItem.GetComponent<itemBucket>()) && o_CManager.isPicked())
            {
                Blocked = true;
                o_itemBucket.AddWater(20);
                yield return new WaitForSeconds(2f);
                Blocked = false;
            }
        }


    }
}
