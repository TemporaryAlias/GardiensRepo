using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerGrowingScript : MonoBehaviour
{
    private int PlantStage;
    void TouchManagement()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit2D RAYHIT;
            Ray2D touchRay = new Ray2D(Input.GetTouch(0).position, transform.forward);

            RAYHIT = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), transform.forward);


            if (RAYHIT.collider.tag == "Flower")
            {


            }
        }
    }
}
