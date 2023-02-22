using System;
using UnityEngine;
using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine.PlayerLoop;
[System.Serializable]
public class shadowDot
    {   
    public Vector3 coordinates;
    public Vector3 position;
    public bool needRender;
    public shadowDot(Vector3 coordinates) {
        this.position = Vector3.zero;
        this.coordinates = coordinates;
        needRender = false;
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

   /* private void FixedUpdate()
    {
        for (int i=0; i< Pixels_.Count;i++)
        {

           
            RaycastHit hit;
            Vector3 ray = (Pixels_[i].coordinates - Light.transform.position).normalized;
            Debug.DrawRay(Light.transform.position, ray * shadowDistance, UnityEngine.Color.yellow);
            // Does the ray intersect any objects excluding the player layer

            if (Physics.Raycast(Light.transform.position, ray, out hit, shadowDistance, shadowMask))
            {

                Pixels_[i].needRender = true;
                Pixels_[i].position = hit.point;
                    Debug.Log("Did Hit");
                GetComponent<PolyDrawer>().AddPoints(Pixels_[i].position);       
            }
            else
            {
                Pixels_[i].needRender = false;
            }
            GetComponent<PolyDrawer>().UpdateFigure();
            
        }
    }*/
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
                bool a = (sprite_new.GetPixel(i, j).a > 0.5);
                if (a)
                {
                   // GameObject go = Instantiate(Pixel, gameObject.transform);
                   // go.transform.position = new Vector3(i, j, 0);
                    shadowDot shadowPart = new shadowDot(new Vector3(i, j, 0));
                    Pixels_.Add(shadowPart);
                    GetComponent<PolyDrawer>().AddPoints(new Vector3(i, j, 0));

                }


            }
        }
        GetComponent<PolyDrawer>().UpdateFigure();





// always top-left color
    }
}


