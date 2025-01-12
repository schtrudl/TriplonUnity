using UnityEngine;

public class DiscCollection : MonoBehaviour
{
    [SerializeField] private float lifetime = 25f; 
    [SerializeField] private float moveHeight = 1.2f; 
    [SerializeField] private float moveSpeed = 0.9f; 
    [SerializeField] private float rotationSpeed = 80f; 

    private Vector3 initialPosition;
    private float timeElapsed;

    private void Start()
    {
        initialPosition = transform.position; 
        Destroy(gameObject, lifetime); // Destroy the disc after the lifetime expires
    }

    private void Update()
    {
        AnimateDisc();
    }

    private void AnimateDisc()
    {
        // Move the disc up and down using a sine wave
        float yOffset = Mathf.Sin(Time.time * moveSpeed) * moveHeight;
        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);

        // Rotate the disc continuously
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure the other object has a PlayerController component in its parent
        PlayerController playerController = other.GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            playerController.CollectDisc();
            Destroy(gameObject); 
        }
    }
}
