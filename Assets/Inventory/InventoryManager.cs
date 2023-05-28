using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> weaponSlots;
    public List<GameObject> supportSlots;
    public List<GameObject> items;
    public Dictionary<GameObject, GameObject> weaponPair = new Dictionary<GameObject, GameObject>();

    float i;
    private void Awake()
    {
        
    }
    private void Start()
    {
        StartCoroutine(wa());
    }
    public void SelectWeapon(GameObject weapon)
    {
        if (weaponPair.ContainsValue(weapon)) UpgradeWeapon(weapon);
        else AddWeapon(weapon);
    }
    public void AddWeapon(GameObject weapon)
    {
        if (weaponPair.Count == weaponSlots.Count) return;
        weaponPair.Add(weaponSlots[weaponPair.Count], weapon);
        Instantiate(weapon);
        weapon.GetComponent<WeaponSuperClass>().init();
    }

    public void UpgradeWeapon(GameObject weapon)
    {
        WeaponSuperClass wsc = weapon.GetComponent<WeaponSuperClass>();
        wsc.UpdateLevel();
    }

    IEnumerator wa()
    {
        yield return new WaitForSeconds(2);
        SelectWeapon(items[0]);
        yield return new WaitForSeconds(2);
        SelectWeapon(items[0]);
    }
}
