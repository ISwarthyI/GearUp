using UnityEngine;

[RequireComponent(typeof(IPlayerInput))] // Bu scriptin IPlayerInput olmadan çalışmasını engeller
public class PlayerInteraction : MonoBehaviour
{
    [Header("Etkileşim Ayarları")]
    public float etkilesimMenzili = 3f;
    public LayerMask etkilesimKatmani;
    public Camera oyuncuKamerasi;

    private IPlayerInput input;

    private void Awake()
    {
        // Girdi arayüzünü kendi üstündeki (veya Player'daki) bileşenden alıyoruz
        input = GetComponent<IPlayerInput>();
    }

    private void Update()
    {
        // Artık Input.GetKeyDown(KeyCode.E) yerine arayüzü kullanıyoruz
        if (input.EtkilesimeGirdiMi)
        {
            EtkilesimDene();
        }
    }

    private void EtkilesimDene()
    {
        Ray ray = new Ray(oyuncuKamerasi.transform.position, oyuncuKamerasi.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, etkilesimMenzili, etkilesimKatmani))
        {
            IInteractable etkilesimliObje = hit.collider.GetComponent<IInteractable>();

            if (etkilesimliObje != null)
            {
                etkilesimliObje.EtkilesimeGec();
            }
        }
    }
}