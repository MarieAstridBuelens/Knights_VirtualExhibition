using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveCanvas : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInChildren<Canvas>(includeInactive: true).gameObject.SetActive(true);
    }
}
