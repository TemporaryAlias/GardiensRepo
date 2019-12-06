using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationScript : MonoBehaviour
{
    [Header("Combination Screen")]
    [SerializeField] Text A_Option;
    [SerializeField] Text B_Option;
    [SerializeField] Image comboImage;
    [SerializeField] Sprite invalidSprite;
    [SerializeField] AudioClip valid;
    [SerializeField] AudioClip invalid;
    [SerializeField] AudioClip combined;
    public AudioSource output;
    //accessing list of names
    playerManager thePM;
    bool soundcheck;


    bool inMenu = false;
    private void Awake()
    {
        thePM = FindObjectOfType<playerManager>();
    }


    private void Update()
    {
        int valA = 0;
        int valB = 0;
        int valC;
        for (int i = 0; i < thePM.FlowerNames.Count; i++)
        {
            if (A_Option.text == thePM.FlowerNames[i])
            {
                valA = i;
            }
            if (B_Option.text == thePM.FlowerNames[i])
            {
                valB = i;
            }
        }


        valC = valA + valB;

        var combo = comboImage.GetComponent<Image>();

        if (valC < thePM.FlowerNames.Count )
        {
            if(PlayerPrefs.GetInt(A_Option.text + "Flower") > 0 && PlayerPrefs.GetInt(B_Option.text + "Flower") > 0 && A_Option.text != B_Option.text)
            {
                output.clip = valid;
                //output.Play();
                GameObject newPlant = (GameObject)Resources.Load("Flowers/" + thePM.FlowerNames[valC]) as GameObject;

                combo.sprite = newPlant.GetComponent<plantItem>().seedStage;
                soundcheck = false;
            }
            if(PlayerPrefs.GetInt(A_Option.text + "Flower") >= 2 && A_Option.text == B_Option.text)
            {
                output.clip = valid;
                GameObject newPlant = (GameObject)Resources.Load("Flowers/" + thePM.FlowerNames[valC]) as GameObject;

                combo.sprite = newPlant.GetComponent<plantItem>().seedStage;
                soundcheck = false;
            }
            else if(PlayerPrefs.GetInt(A_Option.text + "Flower") <= 2 && A_Option.text == B_Option.text || PlayerPrefs.GetInt(A_Option.text + "Flower") <= 0 && PlayerPrefs.GetInt(B_Option.text + "Flower") <= 0 && A_Option.text != B_Option.text)
            {
                if (soundcheck == false)
                {
                    output.clip = invalid;
                    //output.Play();
                    soundcheck = true;
                }
                combo.sprite = invalidSprite;
            }
        }
        else
        {
            if (soundcheck == false)
            {
                output.clip = invalid;
                //output.Play();
                soundcheck = true;
            }
            combo.sprite = invalidSprite;
        }
    }

    public void CombineFlowersButton()
    {
        if (A_Option != null && B_Option != null)
        {
            output.clip = combined;
            output.Play();
            combiningMethod(A_Option, B_Option);
        }
    }



    public void combiningMethod(Text OptionA, Text OptionB)
    {
        if (OptionA != null && OptionB != null)
        {
            //checking if the player has enough stuff
            if (PlayerPrefs.GetInt(OptionA.text + "Flower") > 0 && PlayerPrefs.GetInt(OptionB.text + "Flower") > 0 && OptionA.text != OptionB.text)
            {
                int valA = 0;
                int valB = 0;
                int valC;
                for (int i = 0; i < thePM.FlowerNames.Count; i++)
                {
                    if (OptionA.text == thePM.FlowerNames[i])
                    {
                        valA = i;
                    }
                    else
                    {
                        //valA = 0;
                    }
                    if (OptionB.text == thePM.FlowerNames[i])
                    {
                        valB = i;
                    }
                    else
                    {
                        //valB = 0;
                    }
                }
                //removing flowers for combination

                PlayerPrefs.SetInt(OptionA.text + "Flower", PlayerPrefs.GetInt(OptionA.text + "Flower") - 1);
                PlayerPrefs.SetInt(OptionB.text + "Flower", PlayerPrefs.GetInt(OptionB.text + "Flower") - 1);

                valC = valB + valA;

                //adding a new seed based on combo
                if (PlayerPrefs.GetInt(thePM.FlowerNames[valC] + "Seed") < 1)
                {
                    PlayerPrefs.SetInt(thePM.FlowerNames[valC] + "Seed", 1);
                    thePM.TryProgessTutorial();
                }
                else
                {
                    PlayerPrefs.SetInt(thePM.FlowerNames[valC] + "Seed", PlayerPrefs.GetInt(thePM.FlowerNames[valC] + "Seed") + 1);
                    thePM.TryProgessTutorial();
                }
                //Debug.Log("Combined");
            }

            if (OptionA.text == OptionB.text)
            {
                if (PlayerPrefs.GetInt(OptionA.text + "Flower") > 2 && OptionA.text != OptionB.text)
                {
                    int valA = 0;
                    int valB = 0;
                    int valC;
                    for (int i = 0; i < thePM.FlowerNames.Count; i++)
                    {
                        if (OptionA.text == thePM.FlowerNames[i])
                        {
                            valA = i;
                            valB = i;
                        }
                    }
                    PlayerPrefs.SetInt(OptionA.text + "Flower", PlayerPrefs.GetInt(OptionA.text + "Flower") - 2);

                    valC = valB + valA;

                    //adding a new seed based on combo
                    if (PlayerPrefs.GetInt(thePM.FlowerNames[valC] + "Seed") < 1)
                    {
                        PlayerPrefs.SetInt(thePM.FlowerNames[valC] + "Seed", 1);
                        thePM.TryProgessTutorial();
                    }
                    else
                    {
                        PlayerPrefs.SetInt(thePM.FlowerNames[valC] + "Seed", PlayerPrefs.GetInt(thePM.FlowerNames[valC] + "Seed") + 1);
                        thePM.TryProgessTutorial();
                    }
                }
                
            }

            else
            {
                Debug.Log("You can't cross polinate flowers you don't have!");
            }

        }

    }
}
