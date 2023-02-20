using System;
using UnityEngine;
using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine.PlayerLoop;
[System.Serializable]
public struct shadowDot
    {   
    public Vector3 coordinates;
    public GameObject obj;
    public shadowDot(GameObject obj, Vector3 coordinates) {
        this.obj = obj;
        this.coordinates = coordinates;
    }


}

public class ShadowGPT : MonoBehaviour

{
    public GameObject Light;
    public  Sprite Sprite;
    public int Quality = 5;
    public GameObject Pixel;
    public List<shadowDot> Pixels_;

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
        foreach (var pixels in Pixels_)
        {
          
            RaycastHit hit;
            Vector3 ray = (pixels.coordinates - Light.transform.position).normalized;
            Debug.DrawRay(Light.transform.position, ray * shadowDistance, UnityEngine.Color.yellow);
            // Does the ray intersect any objects excluding the player layer

            if (Physics.Raycast(Light.transform.position, ray, out hit, shadowDistance, shadowMask))
            {
                if (pixels.obj)
                {
                    pixels.obj.SetActive(true);
                    pixels.obj.transform.position = hit.point;
                    Debug.Log("Did Hit");
                }
            }
            else
            {
                pixels.obj.SetActive(false);
            }
            
        }
    }
    private void Start()

    {
        Texture2D sprite_new = GetSlicedSpriteTexture(Sprite);
        shadowDot[][] Pixels = new shadowDot[sprite_new.width][];
        for(int i=0; i < Pixels.Length; i++)
        {
            Pixels[i] = new shadowDot[sprite_new.height];
        }
       

        for (int i=0; i < sprite_new.width;i+=Quality)
        {
           
            for (int j=0; j < sprite_new.height;j+=Quality)
            {
                bool a = (sprite_new.GetPixel(i, j).a > 0.5;
                if ((a && sprite_new.GetPixel(i+1, j).a <= 0.5) || a &&  sprite_new.GetPixel(i-1, j).a <= 0.5) || a )
                {
                    GameObject go = Instantiate(Pixel, gameObject.transform);
                    go.transform.position = new Vector3(i, j, 0);
                    shadowDot shadowPart = new shadowDot(go, go.transform.position);
                    Pixels_.Add(new shadowDot(go, go.transform.position));
                }


            }
        }
       




// always top-left color
        }
}


