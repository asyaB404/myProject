using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryRow : MonoBehaviour
{
    public GameObject rowMark;
    public List<GameObject> slots = new List<GameObject>();

    public async void AddItem(Commodity item)
    {
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<InventorySlot>().curItem == null)
            {
                if(rowMark.activeSelf == false)
                {
                    rowMark.SetActive(true);
                    await Task.Delay(100); // 一种基于“请等一等”的Bug解决方法
                }
                slot.GetComponent<InventorySlot>().SetItem(item);
                slot.SetActive(true);
                break;
            }
            else if (slot.GetComponent<InventorySlot>().curItem.id == item.id)
            {
                slot.GetComponent<InventorySlot>().SetItem(item);
                break;
            }
        }
    }

    public bool HasItem(Commodity item)
    {
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<InventorySlot>().curItem != null && slot.GetComponent<InventorySlot>().curItem.id == item.id)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasEmptySlot()
    {
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<InventorySlot>().curItem == null)
            {
                return true;
            }
        }
        return false;
    }
}
