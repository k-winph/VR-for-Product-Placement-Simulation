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
        yield return new WaitUntil(() => !grab.isSelected);

        grab.transform.SetParent(itemContainer);

        Rigidbody rb = grab.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }


        Debug.Log("Object put in Basket: " + grab.name);
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

            Debug.Log("Object out Basket: " + grab.name);
        }
    }
}
