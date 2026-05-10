using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Input Device")]
    public bool useGamepad = false;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    [Header("Jump & Ground Check")]
    public float jumpPower = 1.5f;
    public float gravity = -9.81f;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundLayer;

    [Header("Mouse Camera Settings")]
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    public bool mouseInvertX = false;
    public bool mouseInvertY = false;

    [Header("Gamepad Camera Settings")]
    public float gamepadSensitivityX = 3f;
    public float gamepadSensitivityY = 2.5f;
    public bool gamepadInvertX = false;
    public bool gamepadInvertY = false;

    [Header("General Camera Limits")]
    public float minLookAngle = -80f;
    public float maxLookAngle = 80f;

    [Header("Tool / Combat Settings")]
    public float attackRate = 0.8f;

    [Header("Keyboard/Mouse Keybindings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode weapon1Key = KeyCode.Alpha1;
    public KeyCode weapon2Key = KeyCode.Alpha2;

    [Header("Gamepad Keybindings")]
    public KeyCode jumpGamepad = KeyCode.JoystickButton0;
    public KeyCode sprintGamepad = KeyCode.JoystickButton8;
    public KeyCode interactGamepad = KeyCode.JoystickButton2;
    public KeyCode attackGamepad = KeyCode.JoystickButton5;
    // Gamepad için silah değiştirme genelde D-Pad ile yapılır ancak şimdilik klavye ile eşdeğer tutmak adına basit butonlar atayabiliriz
    public KeyCode weapon1Gamepad = KeyCode.JoystickButton4; // L1 / Left Bumper
    public KeyCode weapon2Gamepad = KeyCode.JoystickButton5; // R1 / Right Bumper (Eğer attack farklıysa)

    [Header("Gameplay Preferences")]
    public bool toggleSprint = false;
}