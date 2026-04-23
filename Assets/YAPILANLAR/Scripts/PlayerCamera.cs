using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerSettings ayarlar;
    [SerializeField] private Transform oyuncuGovdesi;

    private IPlayerInput input;
    private float xRotasyonu = 0f;

    private void Awake()
    {
        input = oyuncuGovdesi.GetComponent<IPlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        KamerayiDondur();
    }

    private void KamerayiDondur()
    {
        Vector2 bakis = input.KameraGirdisi * ayarlar.fareHassasiyeti;

        xRotasyonu -= bakis.y;

        // Yukarı ve aşağı bakma sınırlarını ayrı değişkenlerle kısıtlıyoruz
        xRotasyonu = Mathf.Clamp(xRotasyonu, ayarlar.yukariBakmaSiniri, ayarlar.asagiBakmaSiniri);

        transform.localRotation = Quaternion.Euler(xRotasyonu, 0f, 0f);
        oyuncuGovdesi.Rotate(Vector3.up * bakis.x);
    }
}