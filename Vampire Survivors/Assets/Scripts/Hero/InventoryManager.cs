using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public List<WeapenController> weaponSlots = new List<WeapenController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);

    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] PassiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);


    public void AddWeapon(int slotIndex, WeapenController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;
        PassiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true;
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            WeapenController weapon = weaponSlots[slotIndex];
            if (!weapon.weaponData.NextLevelPrefab) // if it is null - if there is a next level or not
            {
                Debug.LogError("No next level for " + weapon.weaponData.name);
                return;
            }
            GameObject upgradeedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradeedWeapon.transform.SetParent(transform); // setting thr weappon to be child gor hero
            AddWeapon(slotIndex, upgradeedWeapon.GetComponent<WeapenController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradeedWeapon.GetComponent<WeapenController>().weaponData.Level;
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        if(passiveItemSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveItemSlots[slotIndex];
            if (!passiveItem.passiveItemData.NextLevelPrefab) // if it is null - if there is a next level or not
            {
                Debug.LogError("No next level for " + passiveItem.passiveItemData.name);
                return;
            }
            GameObject upgradeedPasiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradeedPasiveItem.transform.SetParent(transform); // setting thr passive Item to be child gor hero
            AddPassiveItem(slotIndex, upgradeedPasiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            PassiveItemLevels[slotIndex] = upgradeedPasiveItem.GetComponent<PassiveItem>().passiveItemData.Level;
        }
    }
}



