using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{

    [SerializeField] internal PlayerInteraction playerInteraction;
    internal AudioSources audioSource;
    [SerializeField] internal AudioClips audioClip;


    void Update()
    {
        if (playerInteraction.canClick == true)
        {
            Debug.Log("CanClick is true");

            if (Input.GetMouseButtonUp(0))
            {
                audioSource = FindObjectOfType<AudioSources>();
                audioSource.interactionAudioSource.Play();


                //-- Three orders room: Click Object to play sound effects --

                if (playerInteraction.coll.tag == "Cleric Interactible")
                {
                    audioSource.soundIntroAudioSource.clip = audioClip.clericSounds;
                    audioSource.soundIntroAudioSource.Play();
                }

                if (playerInteraction.coll.tag == "Knight Interactible")
                {
                    audioSource.soundIntroAudioSource.clip = audioClip.knightSounds;
                    audioSource.soundIntroAudioSource.Play();
                }

                if (playerInteraction.coll.tag == "Peasant Interactible")
                {
                    audioSource.soundIntroAudioSource.clip = audioClip.peasantSounds;
                    audioSource.soundIntroAudioSource.Play();
                }


                //-- Crusades room: Click Object to display text and lights on map --

                if (playerInteraction.coll.tag == "Hospitaller Interactible")
                {
                    TextMeshPro linkedText = playerInteraction.coll.GetComponentInChildren<TextMeshPro>(includeInactive: true);
                    if (linkedText != null)
                    {
                        linkedText.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("No TextMeshPro found!");
                    }
                }

                if (playerInteraction.coll.tag == "Templar Interactible")
                {
                    TextMeshPro linkedText = playerInteraction.coll.GetComponentInChildren<TextMeshPro>(includeInactive: true);
                    if (linkedText != null)
                    {
                        linkedText.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("No TextMeshPro found!");
                    }
                }

                if (playerInteraction.coll.tag == "Teutonic Interactible")
                {
                    TextMeshPro linkedText = playerInteraction.coll.GetComponentInChildren<TextMeshPro>(includeInactive: true);
                    if (linkedText != null)
                    {
                        linkedText.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("No TextMeshPro found!");
                    }
                }

            }
        }
    }
}
