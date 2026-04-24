using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TasParcasi : MonoBehaviour, IInteractable
{
    public void EtkilesimeGec()
    {
        // Şimdilik envanter olmadığı için sadece yok ediyoruz
        Debug.Log("Taş toplandı.");
        Destroy(gameObject);
    }
}