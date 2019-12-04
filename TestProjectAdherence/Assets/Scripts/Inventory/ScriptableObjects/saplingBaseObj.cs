using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sapling Object", menuName = "Inventory System/FlowerObject/Sapling")]
public class saplingBaseObj : FlowerObject
{
    
    public void Awake()
    {
        typeofFlower = FlowerType.Sapling;
    }
}
