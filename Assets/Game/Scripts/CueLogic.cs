using UnityEngine;

public class CueLogic : MonoBehaviour
{
    
    [SerializeField] private Transform Cue;
    //���������� ��� ���������� ����� � ����
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
        //����������� ��������� ���� � ������� �� ��z
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //������������ y = ����� ��� ���
        mouse.y = transform.position.y;
        //������ ������� �� ��� �� ���
        Vector3 ballDirection = transform.position - Cue.position;
        //�������� �� ����
        Cue.rotation = Quaternion.LookRotation(ballDirection, mouse);
        //��������� ��� �� �����
        Cue.position = mouse;
    }
}
