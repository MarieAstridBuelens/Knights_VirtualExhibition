using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetManager : MonoBehaviour
{
    private IResettable[] resettables;

    private void Awake()
    {
        resettables = GetComponentsInChildren<IResettable>(includeInactive: true);
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedReset());
    }

    //This coroutine gives time on OnEnable to Activate children
    private IEnumerator DelayedReset()
    {
        yield return null; //wait one frame
        foreach (IResettable resettable in resettables)
        {
            resettable.ResetState();
        }
    }
}
