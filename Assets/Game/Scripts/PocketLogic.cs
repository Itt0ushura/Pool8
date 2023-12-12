using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PocketLogic : MonoBehaviour
{
    private Rigidbody _rb; //initiation of rb of ball that collided with pocket
    
    public TextMeshProUGUI ScoreText; //vars for updating score
    private int _score;

    public Button RestartButton; //button which restaring the game when pressed
    void Start()
    {
        ScoreText.text = "0";
        RestartButton.onClick.AddListener(RestartClick);
    }
    private void OnTriggerEnter(Collider other)
    {
        _rb = other.GetComponent<Rigidbody>();
        _score++;
        StartCoroutine(VanishCoolDown(0.1f));//wait 0.1 sec then destroy object and update score
        ScoreText.text = _score.ToString();
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
}