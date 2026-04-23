using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Hareket")]
    public float yurumeHizi = 5f;
    public float kosmaHizi = 8f;

    [Header("Ziplama & Yer Kontrolu")]
    public float ziplamaGucu = 1.5f;
    public float yercekimi = -9.81f;
    public float yerKontrolYaricapi = 0.4f;
    public LayerMask yerKatmani; // Neyin zemin olarak kabul edileceğini seçeceğiz

    [Header("Kamera")]
    public float fareHassasiyeti = 2f;
    public float yukariBakmaSiniri = -80f; // Eksi değerler yukarı bakmayı temsil eder
    public float asagiBakmaSiniri = 80f;   // Artı değerler aşağı bakmayı temsil eder
}