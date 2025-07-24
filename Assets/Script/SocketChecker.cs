using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketChecker : MonoBehaviour
{
    public List<string> fullyCorrectTags = new List<string>();

    public List<string> semiCorrectTags = new List<string>();

    private XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnItemPlaced);
        ScoreManager.RegisterSocket(this);
    }

    private void OnDestroy()
    {
        if (socket != null)
            socket.selectEntered.RemoveListener(OnItemPlaced);
    }

    private void OnItemPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObj = args.interactableObject.transform.gameObject;
        string tag = placedObj.tag;

        if (fullyCorrectTags.Contains(tag))
        {
            Debug.Log("Fully correct (2 points)");
        }
        else if (semiCorrectTags.Contains(tag))
        {
            Debug.Log("Semi correct (1 point)");
        }
        else
        {
            Debug.Log("Incorrect (0 points)");
        }
    }

    public int GetScore()
    {
        if (socket.hasSelection)
        {
            GameObject placedObj = socket.GetOldestInteractableSelected().transform.gameObject;
            string tag = placedObj.tag;

            if (fullyCorrectTags.Contains(tag))
                return 2;
            else if (semiCorrectTags.Contains(tag))
                return 1;
        }

        return 0;
    }
}