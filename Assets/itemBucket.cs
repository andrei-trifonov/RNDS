using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class itemBucket : MonoBehaviour
{
   
    private float waterAmount;
    [SerializeField] private float maxAmount;
    [SerializeField] private Slider waterSlider;
    
    // Start is called before the first frame update


    public void Reset()
    {
        waterAmount = 0;
    }

    public void AddWater(float amount)
    {
        if (waterAmount < maxAmount)
        {
            waterAmount += amount;
            waterSlider.value = waterAmount / maxAmount;
        }
    }

    public float GetWater()
    {
        return waterAmount;
    } 
}
