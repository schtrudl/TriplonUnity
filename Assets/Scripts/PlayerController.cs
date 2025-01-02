using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float turnSpeed = 100f;
    public float tiltAngle = 30f;
    public float tiltSpeed = 2f;

    private float currentTilt = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = 0f;
        float horizontalInput = 0f;

        if (Input.GetKey("w")) verticalInput = 1f;
        if (Input.GetKey("s")) verticalInput = -1f;
        if (Input.GetKey("a")) horizontalInput = -1f;
        if (Input.GetKey("d")) horizontalInput = 1f;

        Vector3 forwardMovement = transform.right * verticalInput * speed * Time.deltaTime;
        transform.position += forwardMovement;

        float rotation = horizontalInput * turnSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        float targetTilt = -horizontalInput * tiltAngle;

        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        Quaternion targetRotation = Quaternion.Euler(currentTilt, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = targetRotation;
    }
}
