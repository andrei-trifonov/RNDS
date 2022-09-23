using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class commonBar : MonoBehaviour
{
    private bool Started;
    private Slider m_Slider;
    private Canvas m_Canvas;
    private bool Finished;
    // Start is called before the first frame update
    void Start()
    {
        m_Slider = gameObject.GetComponent<Slider>();
        m_Canvas = m_Slider.GetComponentInParent<Canvas>();
        m_Canvas.enabled = false;
    }

    public bool isFinished()
    {
        return Finished;
        
    }
    public void FillReset()
    {
        Started = false;
        Finished = false;
        m_Slider.value = 0;
        m_Canvas.enabled = false;
    }

    public void FillStart()
    {
        Started = true;
        m_Canvas.enabled = true;
    }

    public void FillFinished()
    {
        Finished = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Started)
        {
            if (m_Slider.value < 1)
                m_Slider.value += 0.001f;
            else
                FillFinished();
        }
    }
}
