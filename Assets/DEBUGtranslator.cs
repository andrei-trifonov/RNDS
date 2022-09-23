using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUGtranslator : MonoBehaviour
{
    public Slider slider;
   
    private float newval;
    private float newvalT;
    public PanZoom panzoom;
    // Start is called before the first frame update
    void Start()
    {
       
        newval = slider.value;
    }

    // Update is called once per frame
    public void Translate()
    {
        panzoom.zoom(slider.value-newval);
        newval = slider.value;
    }

    public void ChangeTime(bool up)
    {
        if (up)
         Time.timeScale *=2;
        else
        {
            Time.timeScale /=2;
        }
    }
}
