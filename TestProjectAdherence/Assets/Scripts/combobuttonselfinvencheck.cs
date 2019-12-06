using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class combobuttonselfinvencheck : MonoBehaviour
{
    [SerializeField] Text texttoAlter;
    void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt(this.gameObject.name + "Flower").ToString());
        texttoAlter.text = PlayerPrefs.GetInt(this.gameObject.name + "Flower").ToString();
    }
}
