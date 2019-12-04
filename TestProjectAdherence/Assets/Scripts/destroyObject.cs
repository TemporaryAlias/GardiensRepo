using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    public GameObject okButton;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            okButton.SetActive(true);
            Destroy(gameObject);
        }

    }
}
