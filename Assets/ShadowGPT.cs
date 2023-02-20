using System;
using UnityEngine;
using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine.PlayerLoop;

public class ShadowGPT : MonoBehaviour

{
    public GameObject Light;
    public  Sprite Sprite;
    public int Quality = 5;
    public GameObject Pixel;
    public List<GameObject> Pixels;
    public LayerMask shadowMask;
    public float shadowDistance;
    Texture2D GetSlicedSpriteTexture(Sprite sprite)
    {
        Rect rect = sprite.rect;
        Texture2D slicedTex = new Texture2D((int)rect.width, (int)rect.height);
        slicedTex.filterMode = sprite.texture.filterMode;
 
        slicedTex.SetPixels(0, 0, (int)rect.width, (int)rect.height, sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
        slicedTex.Apply();
 
        return slicedTex;
    }

    private void FixedUpdate()
    {
        foreach (var pixels in Pixels)
        {
            RaycastHit hit;
            Debug.DrawRay(Light.transform.position, (pixels.transform.position-Light.transform.position) * shadowDistance, UnityEngine.Color.yellow);
            // Does the ray intersect any objects excluding the player layer
            
            if (Physics.Raycast(Light.transform.position, pixels.transform.position-Light.transform.position , out hit, shadowDistance, shadowMask))
            {
                pixels.transform.position = hit.transform.position;
                Debug.Log("Did Hit");
            }
        }
    }
    private void Start()

    {
        Texture2D sprite_new = GetSlicedSpriteTexture(Sprite);
        for (int i=0; i < sprite_new.width;i+=Quality)
        {
            for (int j=0; j < sprite_new.height;j+=Quality)
            {
                if (sprite_new.GetPixel(i, j).a > 0.5)
                {
                    GameObject go = Instantiate(Pixel, gameObject.transform);
                    go.transform.position = new Vector3(i, j, 0);
                    Pixels.Add(go);
                }
            }
        }

        
          
// always top-left color
    }
}


