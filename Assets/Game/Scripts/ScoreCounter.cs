using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public event Action<int> ScoreUpdater;
    private int _score;
    public void UpdateScore()
    {
        _score++;
        ScoreUpdater?.Invoke(_score);
    }
}