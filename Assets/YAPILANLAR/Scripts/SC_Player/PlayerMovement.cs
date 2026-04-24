using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(IPlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings ayarlar;
    [SerializeField] private Transform yerKontrolNoktasi; // Karakterin ayak ucu

    private CharacterController controller;
    private IPlayerInput input;
    private Vector3 hizVektoru;
    private Vector3 hareketYonu;
    private bool yerdeMi;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<IPlayerInput>();
    }

    private void Update()
    {
        HareketiHesapla();
        YercekimiVeZiplamaUygula();
    }

    private void HareketiHesapla()
    {
        float aktifHiz = input.KosuyorMu ? ayarlar.kosmaHizi : ayarlar.yurumeHizi;

        hareketYonu = transform.right * input.HareketGirdisi.x + transform.forward * input.HareketGirdisi.y;

        if (hareketYonu.magnitude > 1f) hareketYonu.Normalize();

        controller.Move(hareketYonu * (aktifHiz * Time.deltaTime));
    }

    private void YercekimiVeZiplamaUygula()
    {
        // Kendi Ground Check sistemimiz: Belirtilen noktada bir küre oluştur ve yer katmanıyla çarpışmasını kontrol et
        yerdeMi = Physics.CheckSphere(yerKontrolNoktasi.position, ayarlar.yerKontrolYaricapi, ayarlar.yerKatmani);

        if (yerdeMi && hizVektoru.y < 0)
        {
            hizVektoru.y = -2f; // Yere değdiğinde birikmiş yerçekimi hızını sıfırla/sabitle
        }

        // Zıplama koşuluna kendi yerdeMi değişkenimizi bağladık
        if (input.ZipladiMi && yerdeMi)
        {
            hizVektoru.y = Mathf.Sqrt(ayarlar.ziplamaGucu * -2f * ayarlar.yercekimi);
        }

        hizVektoru.y += ayarlar.yercekimi * Time.deltaTime;
        controller.Move(hizVektoru * Time.deltaTime);
    }

    // Geliştirici kolaylığı: Seçili kürenin nerede ve ne kadar büyük olduğunu Editor'de kırmızı bir çizgiyle gösterir
    private void OnDrawGizmos()
    {
        if (yerKontrolNoktasi != null && ayarlar != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(yerKontrolNoktasi.position, ayarlar.yerKontrolYaricapi);
        }
    }
}