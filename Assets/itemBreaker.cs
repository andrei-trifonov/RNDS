using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBreaker : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject Part;
    AudioSource m_audioSource;
    int f = 0;
    private void Start()
    {
        m_audioSource =  GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Moving"))
        {
            f++;
            if (f > 1)
            {
                m_audioSource.PlayOneShot(breakSound);
                for (int i = 0; i < Random.Range(3, 6); i++)
                {
                    GameObject go = Instantiate(Part);
                    go.transform.position = gameObject.transform.position + (Vector3)new Vector2(Random.Range(0f,1f),Random.Range(0f,1f) );
                }
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }

    }
}
