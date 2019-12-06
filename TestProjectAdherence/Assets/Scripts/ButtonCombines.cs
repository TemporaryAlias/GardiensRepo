using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCombines : MonoBehaviour
{
    [SerializeField] Sprite thisSprite;
    [SerializeField] Image otherSprite;
    [SerializeField] Text otherText;
    [SerializeField] Text SeedCountText;
    [SerializeField] GameObject optionToAlter;
    [SerializeField] GameObject parent;
    
    
    public void changeOptionNameSprite()
    {
        otherText.text = this.gameObject.name;
        optionToAlter.gameObject.name = this.gameObject.name;
        otherSprite.sprite = thisSprite;
        parent.SetActive(false);
    }

    
    

    // Update is called once per frame
    void Update()
    {
        SeedCountText.text = PlayerPrefs.GetInt(this.gameObject.name + "Flower").ToString();
    }
}
