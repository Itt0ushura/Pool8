using UnityEngine;

public class CueLogic : MonoBehaviour
{
    
    [SerializeField] private Transform Cue;
    private Vector3 mouse; //var for mouse controll

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
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //convert mouse pos from pixels до xyz
        mouse.y = transform.position.y; //set y = white ball y level
        Vector3 ballDirection = transform.position - Cue.position; //looking for ball direction
        Quaternion cueRotation = Quaternion.LookRotation(ballDirection, mouse); //look on white ball
        Cue.rotation = cueRotation; //rotation of cue around ball
        Vector3 zFreeze = Cue.transform.eulerAngles; //freezing rotation of cue on 0 using euler angles
        zFreeze.z = 0;
        Cue.transform.eulerAngles = zFreeze;
        Cue.position = mouse; //moving Cue to mouse pos
    }
}
