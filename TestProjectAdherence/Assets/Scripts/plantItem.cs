using System.Collections;
using System.Collections.Generic;
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
                    PlayerPrefs.SetInt(speciesNameFlower, PlayerPrefs.GetInt(speciesNameFlower) + 1);
                    PlayerPrefs.SetInt(speciesNameSeed, PlayerPrefs.GetInt(speciesNameSeed) + 2);
                    PlayerPrefs.Save();

                    playerManager.Instance.TryProgessTutorial();

                    if (potParent != null)
                    {
                        potParent.GetComponent<potScript>().isPlanted = false;
                        PlayerPrefs.SetString(potParent.name + "PlantedTrueFalse", "False");
                        PlayerPrefs.SetInt(potParent.name + "FlowerStage", 0);
                    Debug.Log("harvested"+ potParent.name+ "Flowerstage is: " + PlayerPrefs.GetInt(potParent.name + "FlowerStage"));
                        PlayerPrefs.Save();
                        potParent.GetComponent<potScript>().containsFlower = "none";
                        potParent = null;
                    }

                    Destroy(this.gameObject);
                    break;
            }
        
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(this.transform.position, transform.forward * 10f);
    }


}
