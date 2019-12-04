using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(FlowerObject flowerObj, int amountObj)
    {
        bool hasItem = false;
        for (int i =0; i<Container.Count; i++)
        {
            if(Container[i]._flowerObject == flowerObj)
            {
                Container[i].AddAmount(amountObj);
                hasItem = true;
                break;
            }
        }
        if(!hasItem)
        {
            Container.Add(new InventorySlot(flowerObj, amountObj));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public FlowerObject _flowerObject;
    public int _amountofObject;

    public InventorySlot(FlowerObject flowerObj, int amountObj)
    {
        _flowerObject = flowerObj;
        _amountofObject = amountObj;
    }

    public void AddAmount(int valueToAdd)
    {
        _amountofObject += valueToAdd;
    }

}