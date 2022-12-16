using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    Transform mainCamTransform;
    public UnityEvent currentEvent;
    UnityEvent nullEvent;
    RaycastHit hitinfo;
    public float raycastDistance = 5f;

    private void Start()
    {
        mainCamTransform = Camera.main.transform;
        currentEvent = nullEvent;
    }

    private void Update()
    {
        if (Physics.Raycast(mainCamTransform.position, mainCamTransform.forward, out hitinfo, raycastDistance))
        {
            Interactable interactable = hitinfo.collider.GetComponent<Interactable>();
            if (interactable)
            {
                currentEvent = interactable.interactionEvent;
            }
            else
            {
                currentEvent = nullEvent;
            }
        }
        else
        {
            currentEvent = nullEvent;
        }

        if (Input.GetButton("Fire1") && currentEvent!=null)
        {
            currentEvent.Invoke();
            currentEvent = nullEvent;
        }
    }
}
