using UnityEngine;

public class TreeNode : MonoBehaviour, IMineable
{
    [Header("Tree Settings")]
    public ToolType requiredTool = ToolType.Axe;
    public int maxHealth = 5;

    [Header("Growth Settings")]
    public float heightGrowth = 0.4f;     // Sadece gövdenin Y eksenindeki uzama miktarı
    public float thicknessGrowth = 0.05f; // Gövdenin kalınlaşması, kökün ve yaprağın genel büyümesi

    [Tooltip("Eğer TrunkObj için Küp kullanıyorsan 1, Silindir (Cylinder) kullanıyorsan 2 yap.")]
    public float meshBaseHeight = 1f;

    [Header("Hierarchy References")]
    public Transform demolishObj;
    public Transform trunkObj;
    public Transform leaveObj;
    public Transform rootObj;     // YENİ: Kök objesi referansı eklendi

    [Header("Spawning")]
    public GameObject fallenLogPrefab;

    [Header("Audio")]
    public AudioClip chopSound;
    public AudioClip fallSound;

    private int currentHealth;

    // Tüm parçaların orijinal boyut ve pozisyonlarını hafızada tutuyoruz
    private Vector3 origTrunkScale, origTrunkPos;
    private Vector3 origLeaveScale, origLeavePos;
    private Vector3 origRootScale;

    private void Start()
    {
        currentHealth = maxHealth;

        // Objelerin editördeki ilk hallerini kaydediyoruz
        if (trunkObj != null)
        {
            origTrunkScale = trunkObj.localScale;
            origTrunkPos = trunkObj.localPosition;
        }

        if (leaveObj != null)
        {
            origLeaveScale = leaveObj.localScale;
            origLeavePos = leaveObj.localPosition;
        }

        if (rootObj != null)
        {
            origRootScale = rootObj.localScale;
        }

        CalculateInitialSize();
    }

    private void CalculateInitialSize()
    {
        float addedHeight = maxHealth * heightGrowth;
        float addedThickness = maxHealth * thicknessGrowth;

        // 1. GÖVDE (Trunk) BÜYÜMESİ VE KAYDIRILMASI
        if (trunkObj != null)
        {
            trunkObj.localScale = new Vector3(
                origTrunkScale.x + addedThickness,
                origTrunkScale.y + addedHeight,
                origTrunkScale.z + addedThickness
            );

            float yOffset = (addedHeight * meshBaseHeight) / 2f;
            trunkObj.localPosition = origTrunkPos + new Vector3(0, yOffset, 0);
        }

        // 2. YAPRAK (Leave) BÜYÜMESİ VE KAYDIRILMASI
        if (leaveObj != null)
        {
            // Yapraklar her yönden eşit büyüsün ki şekli bozulmasın
            leaveObj.localScale = origLeaveScale + new Vector3(addedThickness, addedThickness, addedThickness);

            // Yaprakları, gövdenin uzadığı miktar kadar yukarı itiyoruz
            leaveObj.localPosition = origLeavePos + new Vector3(0, addedHeight * meshBaseHeight, 0);
        }

        // 3. KÖK (Root) BÜYÜMESİ
        if (rootObj != null)
        {
            // Kökler pozisyon değiştirmez, sadece ağacın kalınlığıyla orantılı olarak her yönden genişler
            rootObj.localScale = origRootScale + new Vector3(addedThickness, 0f, addedThickness);
        }
    }

    public void TakeDamage(int damageAmount, ToolType usedTool)
    {
        if (usedTool != requiredTool) return;

        currentHealth -= damageAmount;

        if (chopSound != null)
            AudioSource.PlayClipAtPoint(chopSound, transform.position);

        if (currentHealth <= 0)
        {
            FellTree();
        }
    }

    private void FellTree()
    {
        if (fallSound != null)
            AudioSource.PlayClipAtPoint(fallSound, transform.position);

        if (fallenLogPrefab != null && demolishObj != null)
        {
            GameObject spawnedLog = Instantiate(fallenLogPrefab, demolishObj.position, demolishObj.rotation);

            FallenLog logScript = spawnedLog.GetComponent<FallenLog>();
            if (logScript != null && trunkObj != null)
            {
                logScript.InitializeLog(maxHealth, trunkObj.localScale);
            }
        }

        if (demolishObj != null) demolishObj.gameObject.SetActive(false);

        Collider myCollider = GetComponent<Collider>();
        if (myCollider != null) myCollider.enabled = false;

        this.enabled = false;
    }
}