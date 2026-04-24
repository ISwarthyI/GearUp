using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    [SerializeField] private PlayerSettings ayarlar;

    private bool kosmaAcikMi = false;

    // Aktif tuşları belirleyen yardımcı özellikler (Properties)
    private KeyCode AktifZiplamaTusu => ayarlar.gamepadKullanilsinMi ? ayarlar.ziplamaGamepad : ayarlar.ziplamaTusu;
    private KeyCode AktifKosmaTusu => ayarlar.gamepadKullanilsinMi ? ayarlar.kosmaGamepad : ayarlar.kosmaTusu;
    private KeyCode AktifEtkilesimTusu => ayarlar.gamepadKullanilsinMi ? ayarlar.etkilesimGamepad : ayarlar.etkilesimTusu;
    // PlayerInputHandler.cs içine diğer aktif tuşların yanına ekle:
    private KeyCode AktifVurmaTusu => ayarlar.gamepadKullanilsinMi ? ayarlar.vurmaGamepad : ayarlar.vurmaTusu;

    // Arayüzden gelen zorunlu özelliği tanımla:
    public bool VurduMu => Input.GetKey(AktifVurmaTusu);

    // Sol analog otomatik olarak bu eksenleri okur
    public Vector2 HareketGirdisi => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    public Vector2 KameraGirdisi
    {
        get
        {
            if (ayarlar.gamepadKullanilsinMi)
            {
                // Sağ analog eksenleri (Unity ayarlarında oluşturacağız)
                return new Vector2(Input.GetAxis("RightStickX"), Input.GetAxis("RightStickY"));
            }
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }

    public bool ZipladiMi => Input.GetKey(AktifZiplamaTusu);

    public bool EtkilesimeGirdiMi => Input.GetKeyDown(AktifEtkilesimTusu);

    public bool KosuyorMu
    {
        get
        {
            if (ayarlar.kosmaBasCekMi)
            {
                if (Input.GetKeyDown(AktifKosmaTusu))
                    kosmaAcikMi = !kosmaAcikMi;

                if (HareketGirdisi.magnitude == 0)
                    kosmaAcikMi = false;

                return kosmaAcikMi;
            }
            else
            {
                return Input.GetKey(AktifKosmaTusu);
            }
        }
    }
}