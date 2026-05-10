// UIManager.cs
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro kullanımı için

public class UIManager : MonoBehaviour
{
    // Inspector'da eşleştirmeyi kolaylaştırmak için bir yapı
    [System.Serializable]
    public struct ResourceUIElements
    {
        public ResourceType type;
        public TextMeshProUGUI amountText;
    }

    public List<ResourceUIElements> resourceUIList;
    private Dictionary<ResourceType, TextMeshProUGUI> uiDictionary;

    private void Start()
    {
        uiDictionary = new Dictionary<ResourceType, TextMeshProUGUI>();

        // Liste elemanlarını Dictionary'e aktar ve başlangıç metinlerini ayarla
        foreach (var item in resourceUIList)
        {
            uiDictionary[item.type] = item.amountText;
            UpdateResourceUI(item.type, InventoryManager.Instance.GetResourceAmount(item.type));
        }

        // InventoryManager'daki Action event'ine abone ol
        InventoryManager.Instance.OnResourceUpdated += UpdateResourceUI;
    }

    private void OnDestroy()
    {
        // Obje silinirse bellek sızıntısını önlemek için abonelikten çık
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnResourceUpdated -= UpdateResourceUI;
        }
    }

    // Event tetiklendiğinde burası çalışır
    private void UpdateResourceUI(ResourceType type, int newAmount)
    {
        if (uiDictionary.ContainsKey(type))
        {
            // Tasarımındaki gibi ": XX" formatında yazdırır
            uiDictionary[type].text = ": " + newAmount.ToString();
        }
    }
}