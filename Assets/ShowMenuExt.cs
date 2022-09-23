using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenuExt : MonoBehaviour
{
    [SerializeField] private Canvas m_Canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Canvas.enabled = false;

    }

    public void Operation(bool status)
    {
        if (status)
            m_Canvas.enabled = true;
        else
            m_Canvas.enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
           
            Operation(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Operation(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
