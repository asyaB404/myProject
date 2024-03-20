using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public Commodity curCommodity;
    
    private void Start()
    {
        GenerateCommodity();
    }

    public void GenerateCommodity()
    {
        curCommodity = ShopManager.instance.getRandomCommodity();

        transform.GetChild(0).GetComponent<Image>().sprite = curCommodity.icon;
        transform.GetChild(1).GetComponent<Text>().text = curCommodity.name;
        transform.GetChild(2).GetComponent<Text>().text = curCommodity.description;
        transform.GetChild(3).GetChild(0).GetComponent<Text>().text = curCommodity.price.ToString();
    }
    
    public void BuyCommodity()
    {
    }
}
