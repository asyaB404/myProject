using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
            }
            return instance;
        }
    }
    public List<Commodity> inventory = new List<Commodity>();
    public List<GameObject> inventoryRows = new List<GameObject>();

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void AddItem(Commodity item)
    {
        if(!inventory.Contains(item))
        {
            inventory.Add(item);
            
        }
        inventory[inventory.IndexOf(item)].holdNum++;
    }

    public void refreshInventoryUI()
    {
        foreach(Commodity item in inventory)
        {
            bool hasItemInRow = false;
            foreach(GameObject row in inventoryRows)
            {
                if(row.GetComponent<InventoryRow>().HasItem(item))
                {
                    row.GetComponent<InventoryRow>().AddItem(item);
                    hasItemInRow = true;
                    break;
                }
            }
            if(hasItemInRow)
            {
                continue;
            }
            else
            {
                foreach(GameObject row in inventoryRows)
                {
                    if(row.GetComponent<InventoryRow>().HasEmptySlot())
                    {
                        row.GetComponent<InventoryRow>().AddItem(item);
                        break;
                    }
                }
            }

            /*
            foreach(GameObject slot in inventorySlots)
            {
                if(slot.GetComponent<InventorySlot>().curItem == null)
                {
                    slot.GetComponent<InventorySlot>().SetItem(item);
                    slot.SetActive(true);
                    break;
                }
                else if(slot.GetComponent<InventorySlot>().curItem.id == item.id)
                {
                    slot.GetComponent<InventorySlot>().SetItem(item);
                    break;
                }
            }
            */
        }
    }
}
