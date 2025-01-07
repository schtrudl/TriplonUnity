using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 25f;
    public float acceleration = 0.2f;
    public float deceleration = 3f;
    public float turnSpeed = 70f;
    public float tiltAngle = 20f;
    public float tiltSpeed = 1.5f;
    public float maxRotationSpeed = 70f;

    private float currentSpeed = 0f;
    private float targetSpeed = 0f;
    private float currentTilt = 0f;
    private float defaultTilt = 0f;
    public float centreOfGravityOffset = -1f;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // enable interpolation for smoother physics
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

        // acceleration and deceleration
        if (verticalInput != 0)
        {
            targetSpeed = verticalInput * maxSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // forward movement
        Vector3 forwardMovement = transform.right * currentSpeed;
        Vector3 velocity = new Vector3(forwardMovement.x, rigidBody.linearVelocity.y, forwardMovement.z);
        rigidBody.linearVelocity = velocity;

        // set turning speed based on current speed
        float dynamicTurnSpeed = turnSpeed * (currentSpeed / maxSpeed);
        dynamicTurnSpeed = Mathf.Clamp(dynamicTurnSpeed, -maxRotationSpeed, maxRotationSpeed);

        if (currentSpeed > 0.1f || currentSpeed < 0.1f)
        {
            // turn
            float rotation = horizontalInput * dynamicTurnSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
            rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);

            // tilt
            float targetTilt = -horizontalInput * tiltAngle;
            currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
        }
        else
        {
            // reset tilt when stopped
            currentTilt = Mathf.Lerp(currentTilt, defaultTilt, Time.deltaTime * tiltSpeed);
        }

        Quaternion tiltRotation = Quaternion.Euler(currentTilt, rigidBody.rotation.eulerAngles.y, rigidBody.rotation.eulerAngles.z);
        rigidBody.MoveRotation(tiltRotation);

    }
}
