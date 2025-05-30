using System.Collections;
using System.Collections.Generic;
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

                if (playerInteraction.coll.tag == "Cleric Interactible")
                {
                    //Debug.Log("soundIntroAudioSource is null? " + (audioSource.soundIntroAudioSource == null));
                    //Debug.Log("clericSounds is null? " + (audioClip.clericSounds == null));
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
            }
        }
    }
}
