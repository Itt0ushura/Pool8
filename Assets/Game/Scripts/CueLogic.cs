using UnityEngine;

public class CueLogic : MonoBehaviour
{
    
    [SerializeField] private Transform Cue;
    //переменные для управления мышью и кием
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
        //конвертація координат миші з пікселей до хуz
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //встановлення y = рівень білої кулі
        mouse.y = transform.position.y;
        //рахуємо відстань від кия до кулі
        Vector3 ballDirection = transform.position - Cue.position;
        //дивимось на кулю
        Cue.rotation = Quaternion.LookRotation(ballDirection, mouse);
        //переміщуємо кий за мишею
        Cue.position = mouse;
    }
}
