using UnityEngine;

public class OOB : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        Debug.Log("" + other.gameObject.name + " is out of bounds (probably hit the wall)");
    }
}
