// InventoryManager.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    // Kaynakları modüler tutmak için Dictionary kullanıyoruz
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    // Sadece kaynak miktarı değiştiğinde UI'ı tetikleyecek Event
    public event Action<ResourceType, int> OnResourceUpdated;

    private void Awake()
    {
        // Singleton yapısı (Her yerden kolayca erişmek için)
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    // Yerdeki eşyayı topladığında (zaten var olan mekaniğinde) bu metodu çağıracaksın
    public void AddResource(ResourceType type, int amount)
    {
        if (resources.ContainsKey(type))
        {
            resources[type] += amount;
        }
        else
        {
            resources[type] = amount;
        }

        // Miktar değişti, UI'a haber ver!
        OnResourceUpdated?.Invoke(type, resources[type]);
    }

    // İleride kule inşa ederken kullanacağın metod
    public bool ConsumeResource(ResourceType type, int amount)
    {
        if (resources.ContainsKey(type) && resources[type] >= amount)
        {
            resources[type] -= amount;
            OnResourceUpdated?.Invoke(type, resources[type]);
            return true; // Kaynak yeterliydi, harcandı
        }
        return false; // Kaynak yetersiz
    }

    public int GetResourceAmount(ResourceType type)
    {
        return resources.ContainsKey(type) ? resources[type] : 0;
    }
}