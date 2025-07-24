using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static List<SocketChecker> allSockets = new List<SocketChecker>();
    public static int totalScore = 0;

    public static void RegisterSocket(SocketChecker socket)
    {
        allSockets.Add(socket);
    }

    public void CalculateScore()
    {
        totalScore = 0;

        foreach (SocketChecker socket in allSockets)
        {
            totalScore += socket.GetScore();
        }

        Debug.Log("Total score: " + totalScore);
    }
}
