using System;
using UnityEngine;

public class MaskSphere3D : MonoBehaviour
{
    public Transform player, mask; // трансформ маски и игрока
    public float maskSize = 4;
    public float maskFade = 10;
    public LayerMask playerMask; // слои, которые нужно игнорировать (например, слой игрока)
    private Vector3 maskScale, maskDir;
    private MaskObject wall;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if col.CompareTag("")
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if col.CompareTag("")
    }