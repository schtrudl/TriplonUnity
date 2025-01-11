using UnityEngine;

public class DiscCollection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<PlayerController>().CollectDisc();

        Destroy(gameObject);
    }
}
