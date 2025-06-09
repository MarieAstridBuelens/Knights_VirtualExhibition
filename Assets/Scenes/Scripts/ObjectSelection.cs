using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.WindowsRuntime;

public class ObjectSelection : MonoBehaviour
{

    [SerializeField] internal PlayerInteraction playerInteraction;
    [SerializeField] internal AudioSources audioSource;
    [SerializeField] internal AudioClips audioClip;
    private bool dragging = false;
    private Transform draggedObject = null;
    private float dragDistance;
    private float fixedZValue;
    [SerializeField] private float collideDistance = 0.3f;


    void Update()
    {
        // Drag stop
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            Debug.Log("Stopping to drag: " + draggedObject.name);

            audioSource = FindObjectOfType<AudioSources>();

            float distanceToSlot = Vector3.Distance(draggedObject.position, draggedObject.GetComponent<SlotPairing>().correctSlot.position);
            if (distanceToSlot < collideDistance)
            {

                audioSource.successOrFailureAudioSource.clip = audioClip.victorySound;
                audioSource.successOrFailureAudioSource.Play();
                draggedObject.position = draggedObject.GetComponent<SlotPairing>().correctSlot.position;
                draggedObject.GetComponent<SlotPairing>().correctSlot.gameObject.SetActive(false);
                draggedObject.GetComponent<SlotPairing>().infoText.gameObject.SetActive(true);
                draggedObject.tag = "Untagged";

                //End of Timeline minigame: main image display
                if (draggedObject.TryGetComponent<SlotsDone>(out SlotsDone _))
                {

                    if (draggedObject.GetComponent<SlotsDone>().data1.tag == "Untagged" && draggedObject.GetComponent<SlotsDone>().data2.tag == "Untagged" && draggedObject.GetComponent<SlotsDone>().data3.tag == "Untagged")
                    {
                        draggedObject.GetComponent<SlotsDone>().mainImage.SetActive(true);
                        draggedObject.GetComponent<SlotsDone>().title.SetActive(true);
                        draggedObject.GetComponent<SlotsDone>().centuries.SetActive(true);
                        audioSource.successOrFailureAudioSource.clip = audioClip.victory2Sound;
                        audioSource.successOrFailureAudioSource.Play();

                    }
                }

                //CoatOfArms minigame goes to next level
                if (draggedObject.parent.TryGetComponent<LevelSwitch>(out LevelSwitch levelSwitch))
                {
                    StartCoroutine(ChangeLevel(levelSwitch));
                }
            }
            else
            {
                audioSource.successOrFailureAudioSource.clip = audioClip.failureSound;
                audioSource.successOrFailureAudioSource.Play();
            }

            dragging = false;
            ResetSlots(draggedObject);
            draggedObject = null;
        }

        if (!playerInteraction.canClick) return;

        //Simple button interactions
        if (Input.GetMouseButtonUp(0) && !Regex.IsMatch(playerInteraction.coll.tag, @"\bGrabbable\b"))
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


        //Drag and Drop game

        // Drag start
        if (Input.GetMouseButtonDown(0) && IsGrabbable(playerInteraction.coll))
        {
            draggedObject = playerInteraction.coll.transform;
            dragDistance = Vector3.Distance(Camera.main.transform.position, draggedObject.position);
            fixedZValue = draggedObject.position.z;
            dragging = true;
            Debug.Log("Starting to drag: " + draggedObject.name);
        }

        // Drag movement
        if (dragging && draggedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPos = ray.GetPoint(dragDistance);

            EnlightSlots(draggedObject);

            //Lock Z pos
            targetPos.z = fixedZValue;

            //Use Rigidbody to avoid enter into walls
            Rigidbody rb = draggedObject.GetComponent<Rigidbody>();
            if (rb != null && rb.isKinematic)
            {
                rb.MovePosition(targetPos);
            }

            else
            {
                draggedObject.position = targetPos; //(won't avoid to get through walls but avoid crashes)
            }
        }


    }

    internal void EnlightSlots(Transform movingObject)
    {
        Transform parent = movingObject.parent;
        if (parent == null) return;

        foreach (Transform child in parent)
        {
            if (child == null) return;

            if (child.TryGetComponent(out Renderer renderer) && child.CompareTag("Slot"))
            {
                renderer.material.SetFloat("_Glow", 5f);
            }
        }
    }

    internal void ResetSlots(Transform movingObject)
    {
        Transform parent = movingObject.parent;
        if (parent == null) return;

        foreach (Transform child in parent)
        {
            if (child == null) return;

            if (child.TryGetComponent(out Renderer renderer) && child.CompareTag("Slot"))
            {
                renderer.material.SetFloat("_Glow", 0f);
            }
        }
    }

    private bool IsGrabbable(Collider col)
    {
        return col != null && Regex.IsMatch(col.tag, @"\bGrabbable\b");
    }

    internal IEnumerator ChangeLevel(LevelSwitch levelSwitchScript)
    {
        yield return new WaitForSeconds(1.2f);
        audioSource.successOrFailureAudioSource.clip = audioClip.victory2Sound;
        audioSource.successOrFailureAudioSource.Play();
        levelSwitchScript.levelToSetVisible.SetActive(true);
        levelSwitchScript.levelToSetInvisible.SetActive(false);
        Debug.Log("A moment has passed");
    }
}
