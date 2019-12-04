using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingTester : MonoBehaviour, IDragHandler, IEndDragHandler
{

    // https://www.youtube.com/watch?v=rjFgThTjLso <------------- source of info for this scripting

    Vector3 menuCenter;
    float DragPer= 0.3f;
    float smoothTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        menuCenter = transform.position;
    }
    
    public void OnDrag(PointerEventData data)
    {
        float dif = data.pressPosition.x - data.position.x;
        transform.position = menuCenter - new Vector3(dif, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float pc = (data.pressPosition.x - data.position.x) / Screen.width;
        if(Mathf.Abs(pc)>DragPer)
        {
            Vector3 movePos = menuCenter;
            if(pc>0)
            {
                
                movePos += new Vector3(-Screen.width, 0, 0); //241 is the panel gap --------> look into setting up correct aspect ratio to use Screen.Width instead
            }
            if(pc<0)
            {
                movePos += new Vector3(Screen.width, 0, 0);
            }
            //transform.position = movePos;
            StartCoroutine(SmoothMoving(transform.position, movePos, smoothTime));
            menuCenter = movePos;
        }
        else
        {
            // transform.position = menuCenter; //reset movement if not over threshold
            StartCoroutine(SmoothMoving(transform.position, menuCenter, smoothTime));
        }
    }

    IEnumerator SmoothMoving(Vector3 start,Vector3 end, float secs)
    {
        float t = 0f;
        while(t<=1.0f)
        {
            t += Time.deltaTime / secs;
            transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    void OpenUpNumberPad()
    {

    }






    /*
     ----------------> possiblity for timing mouse clicks 
     
        var waitTime = 5.0;
        private var timeStamp = Mathf.Infinity;
 
        function Update() 
        {
             if (Input.GetMouseButtonDown(0)) 
             {
                 timeStamp = Time.time + waitTime;
             }
             if (Input.GetMouseButtonUp(0)) 
             {
                 timeStamp = Mathf.Infinity;
             }
     
             if (Time.time >= timeStamp) 
             {
                 Debug.Log("Firing");
                 timeStamp = Time.time + waitTime;
             }
        }

        var waitTime = 5.0;
        var charge = 0.0;
 
        function Update() 
        {
             if (Input.GetMouseButton(0)) 
             {
                 charge += Time.deltaTime;
             }
             if (Input.GetMouseButtonUp(0)) 
             {
                 charge = 0.0;
             }
     
             if (charge >= waitTime) 
             {
                 Debug.Log("Firing");
                 charge = 0.0;
             }
        }

 */

}

