using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float turnSpeed = 100f;
    public float tiltAngle = 30f;
    public float tiltSpeed = 2f;

    private float currentSpeed = 0f;
    private float targetSpeed = 0f;
    private float currentTilt = 0f;
    public float centreOfGravityOffset = -1f;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Enable interpolation for smoother physics
        rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        float verticalInput = 0f;
        float horizontalInput = 0f;

        if (Input.GetKey("w")) verticalInput = 1f;
        if (Input.GetKey("s")) verticalInput = -1f;
        if (Input.GetKey("a")) horizontalInput = -1f;
        if (Input.GetKey("d")) horizontalInput = 1f;

        // Acceleration and deceleration
        if (verticalInput != 0)
        {
            targetSpeed = verticalInput * maxSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Forward movement
        Vector3 forwardMovement = transform.right * currentSpeed;
        Vector3 velocity = new Vector3(forwardMovement.x, rigidBody.linearVelocity.y, forwardMovement.z);
        rigidBody.linearVelocity = velocity;

        // Turning
        float rotation = horizontalInput * turnSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);

        float targetTilt = -horizontalInput * tiltAngle;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);


    }
}
