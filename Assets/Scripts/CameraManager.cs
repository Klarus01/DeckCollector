using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 startPoint = new(0, 0, -10);
    private Camera mainCamera;
    private Vector2 initialCameraLimits = new(4f, 3f);
    private Vector2 cameraLimitIncreasePerWave = new(3f, 2f);
    private float cameraMoveSpeed = 5f;
    
    public Vector2 currentCameraLimits;

    private void Awake()
    {
        currentCameraLimits = initialCameraLimits;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        SetUpCameraToStartingPosition();
    }

    private void Update()
    {
        ClampCameraPosition();
        MoveCameraWithInput();
    }

    private void MoveCameraWithInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
        mainCamera.transform.position += cameraMoveSpeed * Time.deltaTime * moveDirection;
    }

    public void UpdateCameraLimits()
    {
        currentCameraLimits += cameraLimitIncreasePerWave;
    }

    private void ClampCameraPosition()
    {
        var cameraPosition = mainCamera.transform.position;
        var clampedPosition = new Vector3(
            Mathf.Clamp(cameraPosition.x, -currentCameraLimits.x, currentCameraLimits.x),
            Mathf.Clamp(cameraPosition.y, -currentCameraLimits.y, currentCameraLimits.y),
            cameraPosition.z
        );

        mainCamera.transform.position = clampedPosition;
    }

    public void SetUpCameraToStartingPosition()
    {
        if (!mainCamera) return;
        mainCamera.transform.position = startPoint;
    }
}
