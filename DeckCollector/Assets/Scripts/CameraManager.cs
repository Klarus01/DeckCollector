using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector2 initialCameraLimits = new(4f, 3f);
    public Vector2 cameraLimitIncreasePerWave = new(3f, 2f);
    public float cameraMoveSpeed = 5f;

    public Vector2 currentCameraLimits;

    private void Awake()
    {
        currentCameraLimits = initialCameraLimits;
    }

    private void Update()
    {
        ClampCameraPosition();
        MoveCameraWithInput();
    }

    private void MoveCameraWithInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new(horizontalInput, verticalInput, 0f);
        cameraTransform.position += cameraMoveSpeed * Time.deltaTime * moveDirection;
    }

    public void UpdateCameraLimits()
    {
        currentCameraLimits += cameraLimitIncreasePerWave;
    }

    public void ClampCameraPosition()
    {
        Vector3 cameraPosition = cameraTransform.position;
        Vector3 clampedPosition = new(
            Mathf.Clamp(cameraPosition.x, -currentCameraLimits.x, currentCameraLimits.x),
            Mathf.Clamp(cameraPosition.y, -currentCameraLimits.y, currentCameraLimits.y),
            cameraPosition.z
        );

        cameraTransform.position = clampedPosition;
    }
}
