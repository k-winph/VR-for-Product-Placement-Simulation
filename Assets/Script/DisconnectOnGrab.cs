using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisconnectOnGrab : MonoBehaviour
{
    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // แยกออกจาก A
        transform.SetParent(null);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        Debug.Log($"{gameObject.name} ถูกแยกออกจากวัตถุหลัก");
    }
}
