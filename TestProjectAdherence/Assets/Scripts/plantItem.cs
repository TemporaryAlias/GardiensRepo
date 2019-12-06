using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public struct plantStruct
{
    GameObject planttoSpawn;
    string plantName;
    int plantStage;
}

public class plantItem : MonoBehaviour
{

    public Animator anim;

    public bool recentlyGrown;

    public string PLANTNAME;

    public GameObject potParent;

    public Sprite seedStage, saplingStage, flowerStage;

    public int growthStage;

    [SerializeField] string speciesNameFlower, speciesNameSeed;

    public List<Sprite> spritesList;
    private void Awake()
    {
        speciesNameSeed = PLANTNAME + "Seed";
        speciesNameFlower = PLANTNAME + "Flower";

        spritesList = new List<Sprite>();
        spritesList.Add(seedStage);
        spritesList.Add(saplingStage);
        spritesList.Add(flowerStage);
        spritesList[0] = seedStage;
        spritesList[1] = saplingStage;
        spritesList[2] = flowerStage;

        //growthStage = 0;

        anim = GetComponent<Animator>();

        this.GetComponent<SpriteRenderer>().sprite = spritesList[growthStage];
    }

   


    private void Update()
    {
        this.GetComponent<SpriteRenderer>().sprite = spritesList[growthStage];
        potParent.GetComponent<potScript>().flowerStage = growthStage;
        RewardCurveFunction();
    }

    public void AdvanceStage()
    {
            switch (growthStage)
            {
                case 0:
                    growthStage += 1;
                    this.GetComponent<SpriteRenderer>().sprite = spritesList[growthStage];
                    if (potParent != null)
                    {
                        potParent.GetComponent<potScript>().flowerStage = 1;
                        playerManager.Instance.TryProgessTutorial();
                    }
                    break;

                case 1:
                    growthStage += 1;
                    this.GetComponent<SpriteRenderer>().sprite = spritesList[growthStage];
                    if (potParent != null)
                    {
                        potParent.GetComponent<potScript>().flowerStage = 2;
                        playerManager.Instance.TryProgessTutorial();
                    }
                    break;

                case 2:
                    int bonus = bonusFunction();
                    Debug.Log(bonus);
                    PlayerPrefs.SetInt(speciesNameFlower, PlayerPrefs.GetInt(speciesNameFlower) + 1 + bonus);
                    PlayerPrefs.SetInt(speciesNameSeed, PlayerPrefs.GetInt(speciesNameSeed) + 1);
                    
                    PlayerPrefs.Save();
                    
                    playerManager.Instance.TryProgessTutorial();

                    if (potParent != null)
                    {
                        potParent.GetComponent<potScript>().isPlanted = false;
                        PlayerPrefs.SetString(potParent.name + "PlantedTrueFalse", "False");
                        PlayerPrefs.SetInt(potParent.name + "FlowerStage", 0);
                        potParent.GetComponent<potScript>().flowerStage = 0;
                       //Debug.Log("harvested"+ potParent.name+ "Flowerstage is: " + PlayerPrefs.GetInt(potParent.name + "FlowerStage"));
                        PlayerPrefs.Save();
                        potParent.GetComponent<potScript>().containsFlower = "none";
                        potParent = null;
                    }

                    Destroy(this.gameObject);
                    break;
            }
        
       
    }

    float RewardCurveFunction()
    {
        DateTime CurrentTime = DateTime.Now;
        DateTime AlarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, PlayerPrefs.GetInt("Alarm ObjectHour"), PlayerPrefs.GetInt("Alarm ObjectMinute"), DateTime.Now.Second);
        TimeSpan TS = CurrentTime - AlarmTime;
        float secondstohours = (Mathf.Abs(Mathf.Round((float)TS.TotalSeconds))) / 3600;
        float functionResult = Mathf.Exp(-(Mathf.Pow(secondstohours, 2f) / 11f));
        float ReturnedChance = (float)Mathf.Round(functionResult * 100f) / 100f;
        //Debug.Log(ReturnedChance);
        return ReturnedChance;
    }
    public void TestRewardFunction()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 1f);
        float numberToBeat = RewardCurveFunction();

        if(randomNumber<numberToBeat)
        {
            Debug.Log("win reward "+randomNumber+" less than"+numberToBeat);
        }
        else
        {
            Debug.Log("lost reward " + randomNumber + " greater than" + numberToBeat);
        }
        //Debug.Log(numberToBeat);
    }
    int bonusFunction()
    {
        int returnedSeed;
        float randomNumber = UnityEngine.Random.Range(0f, 1f);
        float numberToBeat = RewardCurveFunction();
        if (randomNumber < numberToBeat)
        {
            Debug.Log("win reward " + randomNumber + " less than" + numberToBeat);
            returnedSeed = 1;
        }
        else
        {
            Debug.Log("lost reward " + randomNumber + " greater than" + numberToBeat);
            returnedSeed = 0;
        }
        return returnedSeed;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(this.transform.position, transform.forward * 10f);
    }


}
