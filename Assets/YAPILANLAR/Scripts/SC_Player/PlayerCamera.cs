using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private Transform playerBody;

    private IPlayerInput input;
    private float xRotation = 0f;

    private void Awake()
    {
        input = playerBody.GetComponent<IPlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float sensitivityX = settings.useGamepad ? settings.gamepadSensitivityX : settings.mouseSensitivityX;
        float sensitivityY = settings.useGamepad ? settings.gamepadSensitivityY : settings.mouseSensitivityY;

        bool invertX = settings.useGamepad ? settings.gamepadInvertX : settings.mouseInvertX;
        bool invertY = settings.useGamepad ? settings.gamepadInvertY : settings.mouseInvertY;

        // DÜZELTİLEN KISIM: input.KameraGirdisi -> input.CameraInput
        Vector2 look = input.CameraInput;

        float applyX = look.x * sensitivityX * (invertX ? -1f : 1f);
        float applyY = look.y * sensitivityY * (invertY ? -1f : 1f);

        xRotation -= applyY;
        xRotation = Mathf.Clamp(xRotation, settings.minLookAngle, settings.maxLookAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * applyX);
    }
}