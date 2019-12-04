using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    public int popUpIndex;
    public GameObject Menu;
    public GameObject Alarm;

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (Menu.activeInHierarchy)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Alarm.activeInHierarchy)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Alarm.activeInHierarchy == false)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
    }
}