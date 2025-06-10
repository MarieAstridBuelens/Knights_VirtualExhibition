using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

public class TriggerToAudio : MonoBehaviour
{
    internal AudioSources audioSource;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");

        audioSource = FindObjectOfType<AudioSources>();

        if (gameObject.tag == "Knighting")
        {
            audioSource.knightingAudioSource.Play();
            audioSource.feastAudioSource.Stop();
        }

        if (gameObject.tag == "Feast")
        {
            audioSource.feastAudioSource.Play();
            audioSource.knightingAudioSource.Stop();
        }
        
        if (gameObject.tag == "FinAmor")
        {
            audioSource.finAmorAudioSource.Play();
            audioSource.knightingAudioSource.Stop();
            audioSource.feastAudioSource.Stop();
        }
        
    
    }
}
