using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 25f;
    public float boostSpeed = 40f;
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

    [SerializeField]
    private GameObject discElement;

    // Boost-related variables
    private bool isBoostAvailable = false; // Is the boost available (after disc collection)?
    private bool isBoostActive = false; // Is the boost currently active?
    private float boostRemainingTime = 0f; // Total boost time available
    public float boostFullTime = 2f;
    public float boostSpeedAcceleration = 1f; // Speed multiplier during the boost

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Enable interpolation for smoother physics
        rigidBody.interpolation = RigidbodyInterpolation.Interpolate;

        input = GetComponent<PlayerInput>();
        // Input System has trouble using the same device for multiple players due to XOR access
        // But if we manually bind them it will work
        // https://discussions.unity.com/t/2-players-on-same-input-device/762490/8
        InputUser.PerformPairingWithDevice(Keyboard.current, input.user);
        input.user.ActivateControlScheme(input.defaultControlScheme);

        discElement.SetActive(false);
    }

    void Update()
    {
        // Handle normal movement input
        float verticalInput = 0f;
        float horizontalInput = 0f;
        Vector2 invec = input.actions["MOVE"].ReadValue<Vector2>();
        verticalInput = invec.y;
        horizontalInput = invec.x;

        // Accelerate and decelerate the player based on input
        if (verticalInput != 0)
        {
            targetSpeed = verticalInput * maxSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Handle boost when the key is held down
        if (isBoostAvailable && input.actions["Interact"].IsPressed())
        {
            if (boostRemainingTime > 0f)
            {
                isBoostActive = true;
                currentSpeed = Mathf.Lerp(currentSpeed, boostSpeed, Time.deltaTime * boostSpeedAcceleration);
                boostRemainingTime -= Time.deltaTime; // Decrease available boost time
            }
            else
            {
                // If boost time is depleted, deactivate boost
                DeactivateBoost();
            }
        }
        else
        {
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, deceleration * Time.deltaTime);
            }
        }



        // Apply forward movement
        Vector3 forwardMovement = transform.right * currentSpeed;
        Vector3 velocity = new Vector3(forwardMovement.x, rigidBody.linearVelocity.y, forwardMovement.z);
        rigidBody.linearVelocity = velocity;

        // Set dynamic turning speed based on current speed
        float dynamicTurnSpeed = turnSpeed * (currentSpeed / maxSpeed);
        dynamicTurnSpeed = Mathf.Clamp(dynamicTurnSpeed, -maxRotationSpeed, maxRotationSpeed);

        if (currentSpeed > 0.1f || currentSpeed < -0.1f)
        {
            // Turn the player
            float rotation = horizontalInput * dynamicTurnSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
            rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);

            // Tilt the player
            float targetTilt = -horizontalInput * tiltAngle;
            currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
        }
        else
        {
            // Reset tilt when stopped
            currentTilt = Mathf.Lerp(currentTilt, defaultTilt, Time.deltaTime * tiltSpeed);
        }

        // Apply tilt rotation
        Quaternion tiltRotation = Quaternion.Euler(currentTilt, rigidBody.rotation.eulerAngles.y, rigidBody.rotation.eulerAngles.z);
        rigidBody.MoveRotation(tiltRotation);
    }

    // Deactivate the boost when it's depleted or no longer active
    private void DeactivateBoost()
    {
        isBoostActive = false;
        isBoostAvailable = false; // Reset availability after use
        boostRemainingTime = 0f; // Reset boost timer
        discElement.SetActive(false); // Hide the disc indicator
    }

    // Call this method when the player collects a disc
    public void CollectDisc()
    {
        discElement.SetActive(true);
        isBoostAvailable = true; // Boost becomes available after collecting a disc
        boostRemainingTime = 2f; // Reset boost time when a new disc is collected
    }
}
