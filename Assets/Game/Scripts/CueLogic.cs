using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueLogic : MonoBehaviour
{

    [SerializeField] private Transform Cue;
    [SerializeField] List<Rigidbody> allBalls; //array with rbs of all balls
    private Rigidbody ballRb;
    private Vector3 mouse; //var for mouse controll
    private Vector3 ballDirection; //var for direction of ball


    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Strike();
        Rotation();
    }
    void Rotation()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.y = transform.position.y; //set y = white ball y level

        ballDirection = transform.position - Cue.position;
        Cue.rotation = Quaternion.LookRotation(ballDirection, mouse); //look on white ball
        Vector3 zFreeze = Cue.transform.eulerAngles; //freezing rotation of cue on 0 using euler angles
        zFreeze.z = 0;
        Cue.transform.eulerAngles = zFreeze;
        Cue.position = mouse; //moving Cue to mouse pos
    }
    void Strike()
    {
        if (IsStopped() && Input.GetMouseButtonUp(0))
        {
            ballRb.AddForce(ballDirection.normalized * 6f, ForceMode.Impulse);
            StartCoroutine(StopTimer(10f));
        }

    }
    bool IsStopped()
    {
        foreach (var ball in allBalls)
        {
            if (ball.velocity.magnitude > 0.3) //if at least one of balls is having more than .3 velocity - they are on move
            {
                Cue.gameObject.SetActive(false);
                return false;
            }
        }
        Cue.gameObject.SetActive(true);
        return true;
    }
    IEnumerator StopTimer(float time) //timer for stop motion of balls
    {
        yield return new WaitForSeconds(time);
        foreach (var ball in allBalls)
        {
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
    }
}
