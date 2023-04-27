using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLore : MonoBehaviour
{
    [SerializeField] private Sprite m_Sprite;
    [SerializeField] private bool overrideImage;
    [SerializeField] private string Label;
    [TextArea(15,20)]
    [SerializeField] private string Text;
   

    public string GetLabel()
    {
        return Label;
    }

    public string GetText()
    {
        return Text;
    }
    public Sprite GetSprite()
    {
        if (overrideImage)
          return m_Sprite;
        return transform.parent.GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
