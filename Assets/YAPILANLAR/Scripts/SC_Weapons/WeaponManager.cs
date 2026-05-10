using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Inventory")]
    public List<GameObject> weapons = new List<GameObject>();

    private IPlayerInput input;
    private int activeWeaponIndex = 0;

    private void Awake()
    {
        // Script nerede olursa olsun, en üstteki Player objesinden girdiyi bulur
        input = GetComponentInParent<IPlayerInput>();
        EquipWeapon(activeWeaponIndex);
    }

    private void Update()
    {
        if (input == null) return;

        if (input.SelectedWeapon1) EquipWeapon(0);
        if (input.SelectedWeapon2) EquipWeapon(1);
    }

    private void EquipWeapon(int index)
    {
        // Liste boşsa veya geçersiz bir index girildiyse işlemi iptal et
        if (weapons.Count == 0 || index < 0 || index >= weapons.Count) return;

        activeWeaponIndex = index;

        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].SetActive(i == activeWeaponIndex);
            }
        }
    }
}