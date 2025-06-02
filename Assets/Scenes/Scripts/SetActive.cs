using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    internal Renderer[] renderers;
    void OnTriggerEnter(Collider other)
    {
        renderers = gameObject.GetComponentsInChildren<Renderer>(includeInactive: true);

        foreach (Renderer r in renderers)
        r.gameObject.SetActive(true);
    }

}
