using UnityEngine;

public class KazmaKontrol : MonoBehaviour
{
    [Header("Kazma Ayarları")]
    [SerializeField] private PlayerSettings ayarlar;
    public float vurmaMenzili = 3f;
    public int vurmaGucu = 1;
    public LayerMask madenKatmani;

    [Header("Referanslar")]
    public Animator kazmaAnimator;
    public Camera oyuncuKamerasi;

    // YENİ: Oyuncu girdi arayüzünü tanımlıyoruz
    [SerializeField] private Transform oyuncuGovdesi; // Eğer bu kod kameradaysa ana Player'ı buraya sürükle
    private IPlayerInput input;

    private float sonrakiVurusZamani = 0f;

    private void Awake()
    {
        // Eğer oyuncuGovdesi atanmışsa ondan al, atanmamışsa kendi üstünde ara
        if (oyuncuGovdesi != null)
            input = oyuncuGovdesi.GetComponent<IPlayerInput>();
        else
            input = GetComponentInParent<IPlayerInput>();
    }

    private void Update()
    {
        // ESKİ: Input.GetMouseButtonDown(0)
        // YENİ: input.VurduMu
        if (input != null && input.VurduMu && Time.time >= sonrakiVurusZamani)
        {
            VurmaIsleminiBaslat();
            sonrakiVurusZamani = Time.time + ayarlar.kazmaHizi;
        }
    }

    private void VurmaIsleminiBaslat()
    {
        if (kazmaAnimator != null)
        {
            kazmaAnimator.SetTrigger("Vur");
        }

        MadenKaz();
    }

    private void MadenKaz()
    {
        Ray ray = new Ray(oyuncuKamerasi.transform.position, oyuncuKamerasi.transform.forward);
        RaycastHit vurdugumuzYer;

        if (Physics.Raycast(ray, out vurdugumuzYer, vurmaMenzili, madenKatmani))
        {
            IMineable maden = vurdugumuzYer.collider.GetComponent<IMineable>();
            if (maden != null)
            {
                maden.HasarAl(vurmaGucu);
            }
        }
    }
}