using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class monthSpriteScript : MonoBehaviour
{
    [SerializeField] List<GameObject> listofDays;
    [SerializeField] Sprite taken, notTaken;

    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("replaceSprites", 0f, 5f);
    }

    
    void replaceSprites()
    {
        Debug.Log("Called at: " + System.DateTime.Now);
        foreach(GameObject day in listofDays)
        {
            if(PlayerPrefs.GetString(this.gameObject.name+day.name+"TAKEN")=="TRUE")
            {
                day.gameObject.GetComponent<Button>().image.sprite = taken;
            }
            else
            {
                day.gameObject.GetComponent<Button>().image.sprite = notTaken;
            }
        }
    }

    
}
