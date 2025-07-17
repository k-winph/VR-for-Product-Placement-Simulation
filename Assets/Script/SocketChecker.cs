using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketChecker : MonoBehaviour
{
    public List<string> correctTags = new List<string>();
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

        if (correctTags.Contains(placedObj.tag))
        {
            Debug.Log("correct");
        }
        else
        {
            Debug.Log("incorrect");
        }
    }

    public bool IsCorrect()
    {
        if (socket.hasSelection)
        {
            GameObject placedObj = socket.GetOldestInteractableSelected().transform.gameObject;
            return correctTags.Contains(placedObj.tag);
        }

        return false;
    }
}