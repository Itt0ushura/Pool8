using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CueLogic : MonoBehaviour
{

    [SerializeField] private Transform _cue;
    private List<Rigidbody> _allBalls; //array with rbs of all balls
    private Rigidbody _ballRb;

    private Vector3 _mouse; //var for mouse controll
    private Vector3 _ballDirection; //var for direction of ball

    private Camera _mainCamera;

    public TextMeshProUGUI TimeCount; //var for counting time
    private float _time;
    void Start()
    {
        TimeCount.text = "0"; //set start text of time counter to "0"
        _mainCamera = Camera.main;
        _allBalls = new List<Rigidbody>(); //initiation of a list with balls
        _ballRb = GetComponent<Rigidbody>(); //initiation of whiteball's rb
        var found = FindObjectsOfType<Rigidbody>(); //looking for everything that has a rb
        for (int i = 0; i < found.Length; i++) //adding rb's to a list
        {
            _allBalls.Add(found[i]);
        }
    }
    void Update()
    {
        _time = Time.timeSinceLevelLoad;
        TimeCount.text = "" + Math.Round(_time, 0); 
        Rotation();
        Strike();
        _cue.gameObject.SetActive(IsStopped());
    }
    void Rotation()
    {
        _mouse = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mouse.y = transform.position.y; //set y = white ball y level
        _ballDirection = transform.position - _cue.position;
        _cue.rotation = Quaternion.LookRotation(_ballDirection, _mouse); //look on white ball
        Vector3 zFreeze = _cue.transform.eulerAngles; //freezing rotation of cue on 0 using euler angles
        zFreeze.z = 0;
        _cue.transform.eulerAngles = zFreeze;
        _cue.position = _mouse; //moving Cue to mouse pos
    }
    void Strike()
    {
        if (IsStopped() && Input.GetMouseButtonUp(0))
        {
            _ballRb.AddForce(_ballDirection.normalized * 6f, ForceMode.Impulse); //hitting white ball
            StartCoroutine(StopTimer(5f));
            return;
        }
    }
    bool IsStopped()
    {
        foreach (var ball in _allBalls)
        {
            if (ball != null && ball.velocity.magnitude > 0.01) //if at least one of balls is having more than .1 velocity - they are on move
            {
                return false;
            }
        }
        StopAllBalls();
        return true;
    }
    IEnumerator StopTimer(float time) //timer for stop motion of balls
    {
        yield return new WaitForSeconds(time);
        StopAllBalls();
    }
    void StopAllBalls()
    {
        foreach (var ball in _allBalls)
        {
            if (ball != null)
            {
                ball.velocity = Vector3.zero;
                ball.angularVelocity = Vector3.zero;
            }
        }
    }
}