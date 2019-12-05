using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionScript : MonoBehaviour
{
    playerManager theManager;
    private void Awake()
    {
        theManager = FindObjectOfType<playerManager>();
        Debug.Log(theManager.gameObject.name);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(this.gameObject.name =="Planting Detector")
        {
            if (collision.gameObject != null)
            {
                if (collision.gameObject.tag == "FlowerPot")
                {
                    theManager.GetComponent<playerManager>().PotToAlter = collision.gameObject;
                    //Debug.Log(theManager.GetComponent<playerManager>().PotToAlter);
                }
            }
        }
        if(this.gameObject.name == "Harvest Detector")
        {
            if (collision.gameObject != null)
            {
                if (collision.gameObject.tag == "FlowerObject")
                {
                    theManager.GetComponent<playerManager>().PlantToAdvance = collision.gameObject;
                    //Debug.Log(theManager.GetComponent<playerManager>().PlantToAdvance);
                }
                if (collision.gameObject.tag == "FlowerPot")
                {
                    collision.gameObject.GetComponent<potScript>().isPlanted = false;
                    //Debug.Log(theManager.GetComponent<playerManager>().PotToAlter);
                }
            }
        }
        if (this.gameObject.name == "Growing Detector")
        {
            if (collision.gameObject != null)
            {
                if (collision.gameObject.tag == "FlowerObject")
                {
                    theManager.GetComponent<playerManager>().PlantToAdvance = collision.gameObject;
                    //Debug.Log(theManager.GetComponent<playerManager>().PlantToAdvance);
                }
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlowerPot")
        {
           //theManager.GetComponent<playerManager>().PotToAlter = null;
        }
    }
}
