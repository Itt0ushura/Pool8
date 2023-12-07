using System.Collections;
using UnityEngine;

public class PocketLogic : MonoBehaviour
{
    private Rigidbody rb; //initiation of rb of ball that collided with pocket
    private void OnTriggerEnter(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        StartCoroutine(VanishCoolDown(1f)); //wait 1 sec then destroy object
        return;
    }
    IEnumerator VanishCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        if (rb != null)
        {
            Destroy(rb.gameObject);
        }
    }
}
