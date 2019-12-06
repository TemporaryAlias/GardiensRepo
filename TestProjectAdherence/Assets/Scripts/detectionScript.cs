using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionScript : MonoBehaviour
{
    playerManager theManager;
    public AudioSource output;
    [SerializeField] AudioClip watering;
    [SerializeField] AudioClip harvesting;
    [SerializeField] AudioClip planting;
    [SerializeField] AudioClip growing;
    private void Awake()
    {
        theManager = FindObjectOfType<playerManager>();
        //Debug.Log(theManager.gameObject.name);
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
                    output.clip = planting;
                    output.Play();
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
                    output.clip = harvesting;
                    output.Play();
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
                    output.clip = watering;
                    output.Play();
                    theManager.GetComponent<playerManager>().PlantToAdvance = collision.gameObject;
                    //Debug.Log(theManager.GetComponent<playerManager>().PlantToAdvance);
                    output.clip = growing;
                    output.Play();
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
