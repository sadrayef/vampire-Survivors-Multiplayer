using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    InventoryManager inventory;

    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OpenTreasureChest();
            Destroy(gameObject);
        }
    }

    public void OpenTreasureChest()
    {
        

        if(inventory.GetPossibleEvolutions().Count <= 0)
        {
            Debug.LogWarning("There is no available evolutions!!");
            return; 
        }

        WeaponEvolutionBlueprint toEvolve = inventory.GetPossibleEvolutions()[Random.Range(0, inventory.GetPossibleEvolutions().Count)];
        inventory.EvolveWeapon(toEvolve);

    }
}
