using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragdropScript : MonoBehaviour
{
    float touchStartTime;
    LayerMask rayLayer;
    // Start is called before the first frame update
    void Start()
    {
        rayLayer = LayerMask.GetMask("MovingLayer");
    }

    void Update()
    {
        if (Input.touchCount >= 0)
        {
            
            touchStartTime = Time.time;

            RaycastHit2D RAYHIT;

            RAYHIT = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), transform.forward);

            if (RAYHIT.collider.tag == "FlowerObject")
            {
                //var flowerItemObject = RAYHIT.collider.GetComponent<flowerItem>();
                
                Destroy(RAYHIT.collider.gameObject);
            }


            if (RAYHIT.collider.tag == "Moving")
            {
                Debug.Log("touching Moving");
                GameObject go = RAYHIT.collider.gameObject;

                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    touchStartTime = Time.time;
                }
                
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    go.transform.position = Vector3.Lerp(go.transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector3(0, 0, 10), 0.5f);
                }

                if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                     float touchDelta = Time.time - touchStartTime;
                     Debug.Log(touchDelta);
                    
                }
            }
            
            
            
        }
        
    }
}
