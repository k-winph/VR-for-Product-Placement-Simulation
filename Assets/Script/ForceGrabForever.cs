using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class ForceGrabForever : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private IXRSelectInteractor currentInteractor = null;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.throwOnDetach = false;
        grabInteractable.trackPosition = true;
        grabInteractable.trackRotation = true;

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (currentInteractor == null)
        {
            currentInteractor = args.interactorObject;
        }
        else if (currentInteractor != args.interactorObject)
        {
            XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
            if (interactor != null)
            {
                interactor.interactionManager.SelectExit(interactor, grabInteractable);
            }
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject == currentInteractor)
        {
            XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
            if (interactor != null)
            {
                interactor.interactionManager.SelectEnter(interactor, grabInteractable);
            }
            currentInteractor = null;
        }
    }
}
