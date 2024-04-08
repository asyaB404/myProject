using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Commodity",fileName ="������")]
public class Commodity : PropBase
{
    public int id;
    public int type;
    public string commodityName;
    public Sprite icon;
    public float price;
    public int quality;
    public string description;
}
