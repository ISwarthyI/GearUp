using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Girdi Cihazı")]
    public bool gamepadKullanilsinMi = false;

    [Header("Hareket Ayarları")]
    public float yurumeHizi = 5f;
    public float kosmaHizi = 8f;

    [Header("Ziplama & Yer Kontrolu")]
    public float ziplamaGucu = 1.5f;
    public float yercekimi = -9.81f;
    public float yerKontrolYaricapi = 0.4f;
    public LayerMask yerKatmani;

    // --- YENİ EKLENEN/DÜZENLENEN KAMERA AYARLARI ---
    [Header("Fare (Mouse) Kamera Ayarları")]
    public float fareHassasiyetiX = 2f;
    public float fareHassasiyetiY = 2f;
    public bool fareTersX = false;
    public bool fareTersY = false;

    [Header("Gamepad Kamera Ayarları")]
    public float gamepadHassasiyetiX = 3f;
    public float gamepadHassasiyetiY = 2.5f; // Gamepad'de yukarı/aşağı bakmak genelde daha yavaş ayarlanır
    public bool gamepadTersX = false;
    public bool gamepadTersY = false;

    [Header("Genel Kamera Sınırları")]
    public float yukariBakmaSiniri = -80f;
    public float asagiBakmaSiniri = 80f;

    [Header("Kazma Mekaniği")]
    public float kazmaHizi = 0.8f;

    [Header("Klavye/Fare Tuşları")]
    public KeyCode ziplamaTusu = KeyCode.Space;
    public KeyCode kosmaTusu = KeyCode.LeftShift;
    public KeyCode etkilesimTusu = KeyCode.E;
    public KeyCode vurmaTusu = KeyCode.Mouse0;

    [Header("Gamepad Tuşları")]
    public KeyCode ziplamaGamepad = KeyCode.JoystickButton0;
    public KeyCode kosmaGamepad = KeyCode.JoystickButton8;
    public KeyCode etkilesimGamepad = KeyCode.JoystickButton2;
    public KeyCode vurmaGamepad = KeyCode.JoystickButton5;

    [Header("Oynanış Tercihleri")]
    public bool kosmaBasCekMi = false;
}