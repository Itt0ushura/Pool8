using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PocketLogic : MonoBehaviour
{
    private Rigidbody _rb; //initiation of rb of ball that collided with pocket

    public Button RestartButton; //button which restaring the game when pressed

    public TextMeshProUGUI ScoreText; //vars for updating score

    ScoreCounter _scoreCounter;
    void Start()
    {
        _scoreCounter = FindObjectOfType<ScoreCounter>();
        ScoreCounter.ScoreUpdater += DisplayScore;
        RestartButton.onClick.AddListener(RestartClick);
    }
    private void OnTriggerEnter(Collider other)
    {
        _rb = other.GetComponent<Rigidbody>();
        StartCoroutine(VanishCoolDown(0.1f));//wait 0.1 sec then destroy object and update score
        _scoreCounter.UpdateScore();
        return;
    }
    IEnumerator VanishCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        if (_rb != null)
        {
            Destroy(_rb.gameObject);
        }
    }
    void RestartClick()
    {
        SceneManager.LoadScene("pool8");
    }
    private void DisplayScore(int score)
    {
        ScoreText.text = score.ToString();
    }
}