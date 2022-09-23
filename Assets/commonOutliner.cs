using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonOutliner : MonoBehaviour
{
    public GameObject Visual;
    public Material Outline;
    public Material Normal;
    public bool dontUseOutline;
    public SpriteRenderer[] outlineSprites;
    public void SearchForVisuals()
    {
        if (!dontUseOutline) 
            outlineSprites = Visual.GetComponentsInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    public void EnableOutline()
    {
        if (!dontUseOutline) 
            foreach (var sprite in outlineSprites)
            {
                sprite.material = Outline;
            }
    }

    public void DisableOutline()
    {
        if (!dontUseOutline) 
            foreach (var sprite in outlineSprites)
            {
                sprite.material = Normal;
            }
    }
}
