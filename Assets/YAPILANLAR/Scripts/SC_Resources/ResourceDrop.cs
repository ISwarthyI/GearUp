using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResourceDrop : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Şimdilik envanter olmadığı için sadece yok ediyoruz
        Debug.Log("Resource collected!");
        // Eğer odun topluyorsan:
        InventoryManager.Instance.AddResource(ResourceType.Wood, 1);

        // Eğer taş topluyorsan:
        InventoryManager.Instance.AddResource(ResourceType.Stone, 1);
        Destroy(gameObject);
    }
}