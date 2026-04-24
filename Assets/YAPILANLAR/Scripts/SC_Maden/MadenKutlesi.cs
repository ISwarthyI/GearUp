using UnityEngine;

public class MadenKutlesi : MonoBehaviour, IMineable
{
    [Header("Maden Ayarları")]
    public int maksimumCan = 5;
    public float boyutKatsayisi = 0.2f;
    public float lerpHizi = 8f;

    [Header("Parça Ayarları")]
    public GameObject parcaPrefab;
    [Tooltip("Can başına kaç adet parça düşeceğini belirler. Örn: 0.5 ise 10 can = 5 parça.")]
    public float canBasinaParcaOrani = 0.6f;
    public float sacilmaGucu = 3f;

    [Header("Ses")]
    public AudioClip vurmaSesi;

    private int mevcutCan;
    private Vector3 hedefOlcek;

    private void Start()
    {
        mevcutCan = maksimumCan;
        hedefOlcek = Vector3.one * (mevcutCan * boyutKatsayisi);
        transform.localScale = hedefOlcek;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, hedefOlcek, Time.deltaTime * lerpHizi);
    }

    public void HasarAl(int hasarMiktari)
    {
        mevcutCan -= hasarMiktari;
        hedefOlcek = Vector3.one * (mevcutCan * boyutKatsayisi);

        if (vurmaSesi != null)
            AudioSource.PlayClipAtPoint(vurmaSesi, transform.position);

        if (mevcutCan <= 0)
            Parcalan();
    }

    private void Parcalan()
    {
        // 1. Parça sayısını maksimum cana göre hesapla
        int toplamParcaSayisi = Mathf.CeilToInt(maksimumCan * canBasinaParcaOrani);

        for (int i = 0; i < toplamParcaSayisi; i++)
        {
            // 2. ÇAKIŞMAYI ÖNLEME: Parçayı madenin hacmi içinde rastgele bir noktada doğur
            // Madenin o anki ölçeğine göre küçük bir ofset belirliyoruz
            Vector3 rastgeleOfset = Random.insideUnitSphere * (boyutKatsayisi * 2f);
            // Parçaların yerin dibinde doğmaması için Y eksenini biraz yukarı alıyoruz
            rastgeleOfset.y = Mathf.Abs(rastgeleOfset.y) + 0.5f;

            Vector3 dogumPozisyonu = transform.position + rastgeleOfset;

            GameObject parca = Instantiate(parcaPrefab, dogumPozisyonu, Quaternion.identity);

            // 3. Kontrollü Saçılma
            Rigidbody rb = parca.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Parçaları dışarı doğru iten hafif bir kuvvet
                Vector3 firlatmaYonu = rastgeleOfset.normalized + Vector3.up;
                rb.AddForce(firlatmaYonu * sacilmaGucu, ForceMode.Impulse);

                // Parçaya rastgele bir dönüş (tork) vererek daha doğal durmasını sağla
                rb.AddTorque(Random.insideUnitSphere * sacilmaGucu, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}