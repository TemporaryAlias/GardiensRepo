using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public void CreateObject(GameObject objectToSpawn) {
        Instantiate(objectToSpawn, gameObject.transform.parent);
    }

}
