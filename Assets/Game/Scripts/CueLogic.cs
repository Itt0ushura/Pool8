using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueLogic : MonoBehaviour
{

    [SerializeField] private Transform Cue;
    public static List<Rigidbody> allBalls; //array with rbs of all balls
    private Rigidbody ballRb;
    private Vector3 mouse; //var for mouse controll
    private Vector3 ballDirection; //var for direction of ball

    void Start()
    {
        allBalls = new List<Rigidbody>(); //initiation of a list with balls
        ballRb = GetComponent<Rigidbody>(); //initiation of whiteball's rb
        var found = FindObjectsOfType<Rigidbody>(); //looking for everything that has a rb
        for (int i = 0; i < found.Length; i++) //adding rb's to a list
        {
            allBalls.Add(found[i]);
        }
    }

    void Update()
    {
        Rotation();
        Strike();
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
            ballRb.AddForce(ballDirection.normalized * 6f, ForceMode.Impulse); //hitting white ball
            StartCoroutine(StopTimer(5f));
            return;
        }
    }

    bool IsStopped()
    {
        foreach (var ball in allBalls)
        {
            if (ball != null && ball.velocity.magnitude > 0.01) //if at least one of balls is having more than .1 velocity - they are on move
            {
                Cue.gameObject.SetActive(false);
                return false;
            }
        }
        Stopper();
        Cue.gameObject.SetActive(true);
        return true;
    }

    IEnumerator StopTimer(float time) //timer for stop motion of balls
    {
        yield return new WaitForSeconds(time);
        Stopper();
    }

    void Stopper()
    {
        foreach (var ball in allBalls)
        {
            if (ball != null)
            {
                ball.velocity = Vector3.zero;
                ball.angularVelocity = Vector3.zero;
            }
        }
    }
}