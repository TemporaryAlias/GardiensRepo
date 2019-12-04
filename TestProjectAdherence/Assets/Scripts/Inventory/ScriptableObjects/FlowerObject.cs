using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerType
{
    Seed, Sapling,Flower
}
public enum Species
{
    Rose, Daffodil, Tulip
}


public abstract class FlowerObject : ScriptableObject
{
   
    public GameObject flowerPrefab;
    public FlowerType typeofFlower;
    public Species speciesofFlower;
    public string Description;
}
