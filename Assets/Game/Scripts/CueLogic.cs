using UnityEngine;

public class CueLogic : MonoBehaviour
{
    
    [SerializeField] private Transform Cue;
    //var for mouse controll
    private Vector3 mouse;

    void Start()
    {

    }

    void Update()
    {
        Rotation();
        Debug.DrawLine(Cue.position, Cue.position + Cue.forward * 5, Color.blue);
    }
    void Rotation()
    {
        //convert mouse pos from pixels до xyz
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //set y = white ball y level
        mouse.y = transform.position.y;
        //looking for ball direction
        Vector3 ballDirection = transform.position - Cue.position;
        //look on white ball
        Cue.rotation = Quaternion.LookRotation(ballDirection, mouse);
        //moving Cue to mouse pos
        Cue.position = mouse;
    }
}
