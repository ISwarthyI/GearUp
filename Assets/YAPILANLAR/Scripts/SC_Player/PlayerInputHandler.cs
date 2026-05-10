using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    [SerializeField] private PlayerSettings settings;

    private bool isSprintToggled = false;

    // Aktif tuşları belirleyen yardımcı özellikler (Keyboard vs Gamepad)
    private KeyCode ActiveJumpKey => settings.useGamepad ? settings.jumpGamepad : settings.jumpKey;
    private KeyCode ActiveSprintKey => settings.useGamepad ? settings.sprintGamepad : settings.sprintKey;
    private KeyCode ActiveInteractKey => settings.useGamepad ? settings.interactGamepad : settings.interactKey;
    private KeyCode ActiveAttackKey => settings.useGamepad ? settings.attackGamepad : settings.attackKey;
    private KeyCode ActiveWeapon1Key => settings.useGamepad ? settings.weapon1Gamepad : settings.weapon1Key;
    private KeyCode ActiveWeapon2Key => settings.useGamepad ? settings.weapon2Gamepad : settings.weapon2Key;

    public Vector2 MovementInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    public Vector2 CameraInput
    {
        get
        {
            if (settings.useGamepad)
            {
                return new Vector2(Input.GetAxis("RightStickX"), Input.GetAxis("RightStickY"));
            }
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }

    public bool Jumped => Input.GetKey(ActiveJumpKey);
    public bool Interacted => Input.GetKeyDown(ActiveInteractKey);
    public bool Attacked => Input.GetKey(ActiveAttackKey);
    public bool SelectedWeapon1 => Input.GetKeyDown(ActiveWeapon1Key);
    public bool SelectedWeapon2 => Input.GetKeyDown(ActiveWeapon2Key);

    public bool IsRunning
    {
        get
        {
            if (settings.toggleSprint)
            {
                if (Input.GetKeyDown(ActiveSprintKey))
                    isSprintToggled = !isSprintToggled;

                if (MovementInput.magnitude == 0)
                    isSprintToggled = false;

                return isSprintToggled;
            }
            else
            {
                return Input.GetKey(ActiveSprintKey);
            }
        }
    }
}