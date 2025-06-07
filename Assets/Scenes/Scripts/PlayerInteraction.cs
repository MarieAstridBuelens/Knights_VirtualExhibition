using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float rayLength = 5f;
    internal bool canClick = false;
    [SerializeField] internal Collider savedCollider;
    internal Collider coll;
    private Color col = Color.blue;

    void Start()
    {
        //Hides the cursor
        Cursor.visible = false;
        
        //Keep the mouse on the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        EnlightObject();
    }


    public void EnlightObject()
    {

        Debug.DrawRay(transform.position, -transform.right * rayLength, col);

        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit hit, rayLength))
        {
            coll = hit.collider;

            //if (coll.TryGetComponent(out Renderer renderer) && coll.CompareTag("Interactible"))
            if (coll.TryGetComponent(out Renderer renderer) && (Regex.IsMatch(coll.tag, @"\bInteractible\b")|| Regex.IsMatch(coll.tag, @"\bGrabbable\b")))
            {
                renderer.material.SetFloat("_Glow", 5f);
                canClick = true;
            }

            if (savedCollider != null && (coll == null || coll != savedCollider))
            {
                if (savedCollider.TryGetComponent(out Renderer rend))
                {
                    rend.material.SetFloat("_Glow", 0.0f);
                    canClick = false;
                }
            }

            savedCollider = coll;
        }
    }
}
