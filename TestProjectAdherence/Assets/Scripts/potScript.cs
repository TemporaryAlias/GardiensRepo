using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class potScript : MonoBehaviour
{

    private playerManager theManager;
    
    // flowerStage and containsFlower are for reading and writing to the playerPrefs when the app is quit and restarted. Allows for the persistence of flowers through app quit.
    public string containsFlower;
    public int flowerStage;

    public string spawnThisPlant;
    public bool isPlanted;
    [SerializeField] GameObject plantedObject;
    
    private void Awake()
    {
        theManager = FindObjectOfType<playerManager>();
        isPlanted = false;
        loadData();
    }

    private void Start()
    {
        //loadData();
    }

    public void SpawnNewPlant(string planttoSpawn)
    {
        if (!isPlanted && PlayerPrefs.GetInt(planttoSpawn + "Seed") > 0)
        {
            GameObject newPlant = (GameObject)Resources.Load("Flowers/" + planttoSpawn) as GameObject;
            GameObject v2Plant = Instantiate(newPlant, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3), Quaternion.identity);

            v2Plant.GetComponent<plantItem>().potParent = this.gameObject;
            v2Plant.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3);
            if (PlayerPrefs.GetInt(planttoSpawn + "Seed") > 0 || PlayerPrefs.GetInt(planttoSpawn + "(Clone)Seed") > 0)
            {
                PlayerPrefs.SetInt(this.gameObject.name + planttoSpawn + "GrownToday", DateTime.Today.Day);
                Debug.Log(PlayerPrefs.GetInt(planttoSpawn + "Seed")+"before");
                PlayerPrefs.SetInt(planttoSpawn + "Seed", PlayerPrefs.GetInt(planttoSpawn + "Seed") - 1);
                Debug.Log(PlayerPrefs.GetInt(planttoSpawn + "Seed") + "after");
                isPlanted = true;
                //Instantiate(newPlant);
                v2Plant.name = newPlant.GetComponent<plantItem>().PLANTNAME;
                v2Plant.GetComponent<plantItem>().potParent = this.gameObject;
                v2Plant.GetComponent<plantItem>().growthStage = 0;
                v2Plant.GetComponent<plantItem>().anim.SetTrigger("Plant");
                flowerStage = 0; // <----------------------------------------------------------flower stage is for storing stage of contained flower on app quit
                //PlayerPrefs.SetInt(this.name + "FlowerStage", 0);
                plantedObject = v2Plant;
                containsFlower = planttoSpawn;
                PlayerPrefs.SetString(this.name + "PlantedTrueFalse", "True"); // <---------------------------------------------------------- setting the playerprefbool to true as soon as it is planted

                v2Plant.GetComponent<plantItem>().potParent = this.gameObject;
                if (v2Plant.GetComponent<plantItem>().potParent = this.gameObject)
                {
                    Debug.Log("This should have set the pot parent");
                }
            }
            
        }
    }

    public void saveData()
    {
        PlayerPrefs.SetString(this.name + "Flower", containsFlower);
        if (isPlanted == true)
        {
            PlayerPrefs.SetString(this.name + "PlantedTrueFalse", "True");
            PlayerPrefs.SetInt(this.name + "FlowerStage", flowerStage);
        }
        else if (this.GetComponent<potScript>().isPlanted == false)
        {
            PlayerPrefs.SetString(this.name + "PlantedTrueFalse", "False");
            PlayerPrefs.SetInt(this.name + "FlowerStage", 0);
        }
        PlayerPrefs.Save();
    }

    void loadData()
    {
        if(PlayerPrefs.GetString("Seen Tutorial") != "TRUE")
        {
            PlayerPrefs.DeleteAll();
            Caching.ClearCache();
        }
        Debug.Log("LOaded");
        if (PlayerPrefs.GetString(this.name + "PlantedTrueFalse") == "True")
        {
            foreach (String name in theManager.FlowerNames)
            {
                if (PlayerPrefs.GetString(this.name + "Flower") == name)
                {
                    if (PlayerPrefs.GetInt(this.name + "FlowerStage") >= 0)
                    {
                        GameObject newPlant = (GameObject)Resources.Load("Flowers/" + name) as GameObject;
                        newPlant.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3);
                        newPlant.name = newPlant.GetComponent<plantItem>().PLANTNAME;
                        newPlant.GetComponent<plantItem>().potParent = this.gameObject;
                        isPlanted = true;
                        newPlant.GetComponent<plantItem>().growthStage = PlayerPrefs.GetInt(this.name + "FlowerStage");
                        plantedObject = newPlant;
                        containsFlower = name;
                        this.gameObject.GetComponent<potScript>().plantedObject = newPlant;
                        isPlanted = true;
                        Instantiate(newPlant);
                        
                    }
                }


                
            }
        }
        else if (PlayerPrefs.GetString(this.name + "PlantedTrueFalse") == "False")
        {
            isPlanted = false;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        saveData();
    }

    private void OnApplicationQuit()
    {
        saveData();

        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if(plantedObject=null)
        {
            isPlanted = false;
        }
    }

}
