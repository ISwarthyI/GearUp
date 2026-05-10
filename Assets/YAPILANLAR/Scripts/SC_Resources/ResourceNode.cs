using UnityEngine;

public class ResourceNode : MonoBehaviour, IMineable
{
    [Header("Resource Settings")]
    public ToolType requiredTool = ToolType.Pickaxe;
    public ScaleBehavior scaleBehavior = ScaleBehavior.Uniform; // YENİ: Büyüme davranışı
    public int maxHealth = 5;

    [Tooltip("Minimum scale percentage before destroying (0.1 = 10%)")]
    public float minScalePercent = 0.2f;
    public float lerpSpeed = 8f;

    [Header("Loot Settings")]
    public GameObject dropPrefab;
    public float dropRatePerHealth = 0.6f;
    public float scatterForce = 3f;

    [Header("Audio")]
    public AudioClip hitSound;

    private int currentHealth;
    private Vector3 targetScale;
    private Vector3 originalScale; // YENİ: Objenin sahnedeki ilk halini hafızaya alıyoruz

    private void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;

        UpdateTargetScale();
        transform.localScale = targetScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
    }

    public void TakeDamage(int damageAmount, ToolType usedTool)
    {
        // Doğru alet kontrolü
        if (usedTool != requiredTool)
        {
            Debug.Log($"You need a {requiredTool} to harvest this!");
            return;
        }

        currentHealth -= damageAmount;
        UpdateTargetScale();

        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

        if (currentHealth <= 0)
            Shatter();
    }

    private void UpdateTargetScale()
    {
        // Canın yüzde kaç kaldığını hesapla (Örn: %50 can = 0.5f)
        float healthPercent = (float)currentHealth / maxHealth;

        // Boyutun sıfırlanmasını engelle
        healthPercent = Mathf.Max(healthPercent, minScalePercent);

        // Seçilen davranışa göre yeni boyutu belirle
        if (scaleBehavior == ScaleBehavior.Uniform)
        {
            // Madenler: Her ekseni orantılı küçült
            targetScale = originalScale * healthPercent;
        }
        else if (scaleBehavior == ScaleBehavior.HeightOnly)
        {
            // Ağaçlar: X ve Z (kalınlık) aynı kalsın, sadece Y (boy) kısalsın
            targetScale = new Vector3(originalScale.x, originalScale.y * healthPercent, originalScale.z);
        }
    }

    private void Shatter()
    {
        int totalDrops = Mathf.CeilToInt(maxHealth * dropRatePerHealth);

        for (int i = 0; i < totalDrops; i++)
        {
            // Parçaların objenin orijinal şekli içerisinde rastgele doğmasını sağla
            Vector3 randomOffset = new Vector3(
                Random.Range(-originalScale.x, originalScale.x) * 0.4f,
                Random.Range(0, originalScale.y), // Ağacın dibinden tepesine kadar rastgele bir yer
                Random.Range(-originalScale.z, originalScale.z) * 0.4f
            );

            Vector3 spawnPosition = transform.position + randomOffset;
            GameObject drop = Instantiate(dropPrefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = drop.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 throwDirection = randomOffset.normalized + Vector3.up;
                rb.AddForce(throwDirection * scatterForce, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * scatterForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}