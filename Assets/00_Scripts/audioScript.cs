using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScript : MonoBehaviour
{
    [Header("--------Background Music--------")]
    [SerializeField] AudioSource background;

    public AudioClip backgroundCip;
    void Start()
    {
        background.clip = backgroundCip;
        BackgroundPlay();
    }

    public void BackgroundPlay()
    {
        background.Play();
    }
}
