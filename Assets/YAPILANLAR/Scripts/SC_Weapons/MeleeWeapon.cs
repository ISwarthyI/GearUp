using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Tool Settings")]
    public ToolType toolType; // Inspector'dan Pickaxe veya Axe seç!
    public float attackRate = 0.8f;
    public float attackRange = 3f;
    public int attackDamage = 1;
    public LayerMask targetLayer;

    [Header("References")]
    public Animator weaponAnimator;
    public Camera playerCamera;

    private IPlayerInput input;
    private float nextAttackTime = 0f;

    private void Awake()
    {
        input = GetComponentInParent<IPlayerInput>();
    }

    private void Update()
    {
        if (input != null && input.Attacked && Time.time >= nextAttackTime)
        {
            PerformAttack();
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void PerformAttack()
    {
        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("Attack"); // Animatördeki parametrenin adını "Attack" olarak değiştir!
        }

        MineResource();
    }

    private void MineResource()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, targetLayer))
        {
            IMineable resource = hit.collider.GetComponent<IMineable>();
            if (resource != null)
            {
                resource.TakeDamage(attackDamage, toolType);
            }
        }
    }
}