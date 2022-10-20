using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLore : MonoBehaviour
{
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
}
