using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisconnectOnGrab : MonoBehaviour
{
    private XRGrabInteractable grab;
    private ObjectConnector connector;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        connector = GetComponent<ObjectConnector>();
        grab.selectExited.AddListener(OnDetach);
    }

    private void OnDestroy()
    {
        grab.selectExited.RemoveListener(OnDetach);
    }

    private void OnDetach(SelectExitEventArgs args)
    {
        transform.SetParent(null);

        if (connector != null)
        {
            connector.ResetConnection();
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log($"{gameObject.name} detached from basket");
    }
}
