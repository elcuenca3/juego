using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioPlayer;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="cuerpo"){
         audioPlayer.Play();
        }
    }
}
