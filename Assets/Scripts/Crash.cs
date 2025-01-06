using UnityEngine;

public class Crash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("" + other.gameObject.name + " crashed into Trail");
    }
}
