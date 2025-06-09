using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettableObject : MonoBehaviour, IResettable
{
    private Vector3 initialPosition;
    private string initialTag;
    private bool initialActiveState;

    void Awake()
    {
        initialPosition = transform.position;
        if (gameObject.tag != null)
        {
            initialTag = gameObject.tag;
        }
        initialActiveState = gameObject.activeSelf;
        
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        if (initialTag != null)
        {
            gameObject.tag = initialTag;
        }
        gameObject.SetActive(initialActiveState);
    }
}
