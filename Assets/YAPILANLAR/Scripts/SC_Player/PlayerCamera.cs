using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerSettings ayarlar;
    [SerializeField] private Transform oyuncuGovdesi;

    private IPlayerInput input;
    private float xRotasyonu = 0f;

    private void Awake()
    {
        input = oyuncuGovdesi.GetComponent<IPlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        KamerayiDondur();
    }

    private void KamerayiDondur()
    {
        // 1. Hangi cihazın kullanıldığına göre X ve Y hassasiyetlerini belirle
        float hassasiyetX = ayarlar.gamepadKullanilsinMi ? ayarlar.gamepadHassasiyetiX : ayarlar.fareHassasiyetiX;
        float hassasiyetY = ayarlar.gamepadKullanilsinMi ? ayarlar.gamepadHassasiyetiY : ayarlar.fareHassasiyetiY;

        // 2. Hangi cihazın kullanıldığına göre Invert (Ters) ayarlarını belirle
        bool tersX = ayarlar.gamepadKullanilsinMi ? ayarlar.gamepadTersX : ayarlar.fareTersX;
        bool tersY = ayarlar.gamepadKullanilsinMi ? ayarlar.gamepadTersY : ayarlar.fareTersY;

        // 3. Girdiyi al
        Vector2 bakis = input.KameraGirdisi;

        // 4. Hassasiyet ve Ters Çevirme (+/-) işlemlerini girdiye uygula
        // Ters ayarı açıksa -1 ile çarpıyoruz, değilse 1 ile çarpıp normal bırakıyoruz
        float uygulanacakX = bakis.x * hassasiyetX * (tersX ? -1f : 1f);
        float uygulanacakY = bakis.y * hassasiyetY * (tersY ? -1f : 1f);

        // 5. Yukarı/Aşağı Bakma İşlemi (X Ekseninde Dönüş)
        xRotasyonu -= uygulanacakY;
        xRotasyonu = Mathf.Clamp(xRotasyonu, ayarlar.yukariBakmaSiniri, ayarlar.asagiBakmaSiniri);

        transform.localRotation = Quaternion.Euler(xRotasyonu, 0f, 0f);

        // 6. Sağa/Sola Dönme İşlemi (Oyuncu Gövdesini Y Ekseninde Döndür)
        oyuncuGovdesi.Rotate(Vector3.up * uygulanacakX);
    }
}