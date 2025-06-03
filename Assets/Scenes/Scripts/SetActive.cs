using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetActive : MonoBehaviour
{

    [SerializeField] private GameObject targetRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetRoom.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetRoom.SetActive(false);
        }
    }



    //Ancien système (ne supporte pas les allers-retours)

    // internal Renderer[] renderers;
    // void OnTriggerEnter(Collider other)
    // {
    //     renderers = gameObject.GetComponentsInChildren<Renderer>(includeInactive: true);

    //     foreach (Renderer r in renderers)
    //     r.gameObject.SetActive(true);
    // }

}
