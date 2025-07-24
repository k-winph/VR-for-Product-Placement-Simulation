using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectConnector : MonoBehaviour
{
    public string targetTag = "Connectable";

    private bool isConnected = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isConnected) return;

        if (collision.gameObject.CompareTag(targetTag))
        {
            XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
            if (grab != null)
            {
                StartCoroutine(WaitForReleaseAndConnect(grab, collision.transform));
            }
        }
    }

    private IEnumerator WaitForReleaseAndConnect(XRGrabInteractable grab, Transform parent)
    {
        yield return new WaitUntil(() => !grab.isSelected);

        transform.SetParent(parent);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        isConnected = true;
        Debug.Log($"{gameObject.name} join {parent.name}");
    }

    public void ResetConnection()
    {
        isConnected = false;
    }

}
