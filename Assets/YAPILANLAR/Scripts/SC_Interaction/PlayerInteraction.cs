using UnityEngine;

[RequireComponent(typeof(IPlayerInput))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 3f;
    public LayerMask interactionLayer;
    public Camera playerCamera;

    private IPlayerInput input;

    private void Awake()
    {
        input = GetComponent<IPlayerInput>();
    }

    private void Update()
    {
        // Girdi arayüzündeki İngilizce isimlendirmeyi (Interacted) kullanıyoruz
        if (input != null && input.Interacted)
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactionLayer))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();

            if (interactableObject != null)
            {
                interactableObject.Interact();
            }
        }
    }
}