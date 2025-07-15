using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class BasketTrigger : MonoBehaviour
{
    public Transform itemContainer;

    private void OnTriggerEnter(Collider other)
    {
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            StartCoroutine(WaitUntilReleasedAndAddToBasket(grab));
        }
    }

    private IEnumerator WaitUntilReleasedAndAddToBasket(XRGrabInteractable grab)
    {
        // รอจนกว่าผู้เล่นจะปล่อยวัตถุ
        yield return new WaitUntil(() => !grab.isSelected);

        // ย้ายเข้าเป็นลูกของตะกร้า
        grab.transform.SetParent(itemContainer);

        // ปิดฟิสิกส์ให้ติดกับตะกร้า
        Rigidbody rb = grab.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // ปิดการจับซ้ำในทันที (ถ้าต้องการ)
        // grab.enabled = false;

        Debug.Log("ของถูกใส่ตะกร้า: " + grab.name);
    }

    private void OnTriggerExit(Collider other)
    {
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.transform.SetParent(null);

            Rigidbody rb = grab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("ของถูกหยิบออกจากตะกร้า: " + grab.name);
        }
    }
}
