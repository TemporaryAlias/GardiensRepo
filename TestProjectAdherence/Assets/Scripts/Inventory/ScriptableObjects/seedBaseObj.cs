using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed Object", menuName = "Inventory System/FlowerObject/Seed")]
public class seedBaseObj : FlowerObject
{
    public void Awake()
    {
        typeofFlower = FlowerType.Seed;
    }
}
