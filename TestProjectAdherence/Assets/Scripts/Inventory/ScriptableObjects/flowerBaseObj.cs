using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flower Object", menuName = "Inventory System/FlowerObject/Flower")]
public class flowerBaseObj : FlowerObject
{
    public void Awake()
    {
        typeofFlower = FlowerType.Flower;
    }
}
