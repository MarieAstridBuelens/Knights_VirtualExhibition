using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLevelOneButton : MonoBehaviour
{
    [SerializeField] internal GameObject levelsParent;
    [SerializeField] internal GameObject resetToThisLevel;
    private void OnMouseUp()
    {
        //find current active level and desactivate it
        foreach (Transform child in levelsParent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        //activate level1
        resetToThisLevel.SetActive(true);
    }
}
