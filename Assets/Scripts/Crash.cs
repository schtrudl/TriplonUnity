using TMPro;
using UnityEngine;

public class Crash : MonoBehaviour
{
    public End endMenu;

    private void Start()
    {
        endMenu = Resources.FindObjectsOfTypeAll<End>()[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        endMenu.end(other.gameObject.transform.parent.name + " crashed into Trail");
    }
}
