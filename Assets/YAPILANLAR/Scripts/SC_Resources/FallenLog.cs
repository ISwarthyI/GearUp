using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallenLog : MonoBehaviour, IMineable
{
    [Header("Log Settings")]
    public ToolType requiredTool = ToolType.Axe;

    [Header("Loot Settings")]
    public GameObject woodDropPrefab;
    public float dropRatePerHealth = 1f; // 1 Health = 1 Odun Parçası
    public float scatterForce = 4f;

    [Header("Audio")]
    public AudioClip chopSound;
    public AudioClip shatterSound;

    private int maxHealth;
    private int currentHealth;
    private bool isInitialized = false;

    // YENİ: Ağaç kesildiğinde bu fonksiyonu çağırıp kütüğe bilgi aktarır
    public void InitializeLog(int inheritedHealth, Vector3 inheritedScale)
    {
        maxHealth = inheritedHealth;
        currentHealth = maxHealth;
        transform.localScale = inheritedScale; // Boyutu ağaçtan al
        isInitialized = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse);
        }
    }

    private void Start()
    {
        // Eğer sahnede test için elle koyduğumuz bir kütük varsa çalışması için güvenlik
        if (!isInitialized)
        {
            maxHealth = 4; // Varsayılan değer
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damageAmount, ToolType usedTool)
    {
        if (usedTool != requiredTool) return;

        currentHealth -= damageAmount; // Hasar alıyor ama küçülmüyor

        if (chopSound != null)
            AudioSource.PlayClipAtPoint(chopSound, transform.position);

        if (currentHealth <= 0)
        {
            BreakIntoWood();
        }
    }

    private void BreakIntoWood()
    {
        if (shatterSound != null)
            AudioSource.PlayClipAtPoint(shatterSound, transform.position);

        // Kütüğün aktarılan canına göre düşecek net kaynak miktarını hesapla
        int totalDrops = Mathf.CeilToInt(maxHealth * dropRatePerHealth);

        for (int i = 0; i < totalDrops; i++)
        {
            // Odunları kütüğün o anki fiziksel uzunluğuna göre dağıtarak spawnla
            Vector3 randomOffset = new Vector3(
                Random.Range(-transform.localScale.x * 0.3f, transform.localScale.x * 0.3f),
                Random.Range(-transform.localScale.y * 0.4f, transform.localScale.y * 0.4f),
                Random.Range(-transform.localScale.z * 0.3f, transform.localScale.z * 0.3f)
            );

            Vector3 spawnPosition = transform.position + randomOffset;
            GameObject drop = Instantiate(woodDropPrefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = drop.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce((Vector3.up + Random.insideUnitSphere * 0.5f) * scatterForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}