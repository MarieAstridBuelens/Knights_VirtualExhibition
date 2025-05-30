using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerToText : MonoBehaviour
{
    internal AudioSources audioSource;
    [SerializeField] internal TextMeshPro tmp_panelText;

    //Autre façon de récupérer le texte
    //void Start()
    //{
    //    tmp_panelText = gameObject.GetComponentInChildren<TextMeshPro>(includeInactive: true);
    //}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");


        audioSource = FindObjectOfType<AudioSources>();
        audioSource.interactionAudioSource.Play();

        tmp_panelText.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggered to exit");
        
        //audioSource = FindObjectOfType<AudioSources>();
        //audioSource.interactionAudioSource.Play();

        tmp_panelText.gameObject.SetActive(false);
    }
    

}
