using TMPro;
using UnityEngine;

public class OOB : MonoBehaviour
{
    private End endMenu;

    private void Start()
    {
        endMenu = Resources.FindObjectsOfTypeAll<End>()[0];
    }
    void OnTriggerExit(Collider other)
    {
        endMenu.end($"{other.gameObject.transform.parent.name} hit the wall!");
    }
}
