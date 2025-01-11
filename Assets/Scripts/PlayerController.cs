using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 25f;
    public float acceleration = 0.2f;
    public float deceleration = 3f;
    public float turnSpeed = 70f;
    public float tiltAngle = 10f;
    public float tiltSpeed = 1.5f;
    public float maxRotationSpeed = 70f;

    private float currentSpeed = 0f;
    private float targetSpeed = 0f;
    private float currentTilt = 0f;
    private float defaultTilt = 0f;
    public float centreOfGravityOffset = -1f;
    private Rigidbody rigidBody;
    private PlayerInput input;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // enable interpolation for smoother physics
        rigidBody.interpolation = RigidbodyInterpolation.Interpolate;

        input = GetComponent<PlayerInput>();
        // Input System has trouble using same device for multiple players due to xor acccess
        // but if we manually bind them it will work
        // https://discussions.unity.com/t/2-players-on-same-input-device/762490/8
        InputUser.PerformPairingWithDevice(Keyboard.current, input.user);
        input.user.ActivateControlScheme(input.defaultControlScheme);
    }

    void Update()
    {
        float verticalInput = 0f;
        float horizontalInput = 0f;
        Vector2 invec = input.actions["MOVE"].ReadValue<Vector2>();
        verticalInput = invec.y;
        horizontalInput = invec.x;

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
