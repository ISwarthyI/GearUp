using UnityEngine;

public interface IPlayerInput
{
    Vector2 MovementInput { get; }
    Vector2 CameraInput { get; }
    bool IsRunning { get; }
    bool Jumped { get; }
    bool Interacted { get; }
    bool Attacked { get; }
    bool SelectedWeapon1 { get; } // Silah 1 tuşu
    bool SelectedWeapon2 { get; } // Silah 2 tuşu
}