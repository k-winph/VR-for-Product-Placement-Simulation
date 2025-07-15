using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectConnector : MonoBehaviour
{
    [Tooltip("Tag ของวัตถุเป้าหมายที่จะเชื่อมต่อด้วย เช่น 'Connectable'")]
    public string targetTag = "Connectable";

    private bool isConnected = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isConnected) return;

        if (collision.gameObject.CompareTag(targetTag))
        {
            // รอจนกว่าผู้เล่นจะปล่อยมือก่อนค่อยเชื่อม
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

        // เชื่อมต่อกับ A
        transform.SetParent(parent);

        // ปิด Rigidbody เพื่อให้ติดแน่น
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        isConnected = true;
        Debug.Log($"{gameObject.name} เชื่อมกับ {parent.name}");
    }
}
