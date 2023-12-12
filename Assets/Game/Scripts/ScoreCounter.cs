using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static event Action<int> ScoreUpdater;
    private int _score;
    public void UpdateScore()
    {
        _score++;
        ScoreUpdater?.Invoke(_score);
    }
}